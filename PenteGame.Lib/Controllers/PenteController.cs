using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using System;
using System.Collections.Generic;

namespace PenteGame.Lib.Controllers
{
    public class PenteController
    {
        private const int MaxDistanceForCapture = 4;
        private const int ClosingBracketDistance = 3;
        public event Action<PieceColor> Tessara;
        public event Action<PieceColor> Tria;
        public event Action<PieceColor> Win;
        public event Action<PieceColor> Capture;
        private readonly IDictionary<Point, GamePiece> _board;
        private readonly IDictionary<PieceColor, int> _captures;
        private int _height;
        private int _width;
        private bool firstMoveMade;
        public int Width
        {
            get => _width;
            set
            {
                if (value % 2 == 1)
                {
                    _width = value;
                }
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                if (value % 2 == 1)
                {
                    _height = value;
                }
            }
        }

        public PieceColor CurrentTurn { get; set; }
        public IEnumerable<GamePiece> Pieces => _board.Values;

        public PenteController()
        {
            this.firstMoveMade = false;
            _board = new Dictionary<Point, GamePiece>();
            _captures = new Dictionary<PieceColor, int>();
            Width = 19;
            Height = 19;
            _captures[PieceColor.Black] = 0;
            _captures[PieceColor.White] = 0;
            Capture += (color) => 
            {
                _captures[color]++;
                if (_captures[color] >= 5)
                {
                    Win?.Invoke(color);
                }
            };
        }

        public int GetTotalCaptures(PieceColor color)
        {
            int totalCaptures = 0;

            if (_captures.ContainsKey(color))
            {
                totalCaptures = _captures[color];
            }

            return totalCaptures;
        }

        public bool TakeTurn(Point placement, PieceColor color) //Returns bool result depending on the validity of the position
        {
            bool isValidMove = false;
            if (!_board.ContainsKey(placement) &&
                color == CurrentTurn &&
                IsOnBoard(placement) &&
                ((!firstMoveMade && CheckIfCenterPoint(placement)) || firstMoveMade))
            {
                firstMoveMade = true;
                isValidMove = true;
                _board[placement] = new GamePiece(placement, color);
                CoordinateMoves(placement);
                SwitchTurn();
            }
            return isValidMove;
        }

        private bool CheckIfCenterPoint(Point placement)
        {
            Point center = new Point(Width/2, Height/2);
            return center == placement;
        }

        private void SwitchTurn()
        {
            CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        private void CoordinateMoves(Point placement)
        {
            CheckVerticalCapture(placement);
            CheckDiagonalRightCapture(placement);
            CheckDiagonalLeftCapture(placement);
            CheckHorizontalCapture(placement);
            CheckHorizontalGroupings(placement);
            CheckVerticalGroupings(placement);
            CheckDiagonalUpperRightGroupings(placement);
            CheckDiagonalUpperLeftGroupings(placement);
        }

        private bool CheckVerticalCapture(Point placement) //Check if a piece could be captured vertically and captures if true
        {
            bool hasBeenAVerticalCapture = ProccessCapture(placement, (point, mod) => point.AddToY(mod));
            hasBeenAVerticalCapture = ProccessCapture(placement, (point, mod) => point.AddToY(-mod)) || hasBeenAVerticalCapture;
            return hasBeenAVerticalCapture;
        }

        private bool CheckHorizontalCapture(Point newestPiece)
        {
            bool horizontalCaptureHasBeenFound = ProccessCapture(newestPiece, (point, mod) => point.AddToX(mod));
            horizontalCaptureHasBeenFound = ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod)) || horizontalCaptureHasBeenFound;
            return horizontalCaptureHasBeenFound;
        }


        private bool CheckDiagonalRightCapture(Point newestPiece)
        {
            bool horizontalCaptureHasBeenFound = ProccessCapture(newestPiece, (point, mod) => point.AddToX(mod).AddToY(mod));
            horizontalCaptureHasBeenFound = ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod).AddToY(-mod)) || horizontalCaptureHasBeenFound;
            return horizontalCaptureHasBeenFound;
        }

        private bool CheckDiagonalLeftCapture(Point newestPiece)
        {
            bool horizontalCaptureHasBeenFound = ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod).AddToY(mod));
            horizontalCaptureHasBeenFound = ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod).AddToY(mod)) || horizontalCaptureHasBeenFound;
            return horizontalCaptureHasBeenFound;
        }

        private int CaptureMethod(Point basePoint, Func<Point, int, Point> getNextPoint)
        {
            int distanceCounter = 0;
            Point positionDown;
            do
            {
                positionDown = getNextPoint(basePoint, ++distanceCounter);

            } while (distanceCounter < MaxDistanceForCapture &&
                    CheckIfPointExists(positionDown) &&
                    (CheckIfOppositePiece(positionDown, _board[basePoint].Color, distanceCounter) ||
                    CheckIfJoiningPiece(positionDown, _board[basePoint].Color, distanceCounter)));

            return distanceCounter;
        }

        private bool ProccessCapture(Point ogPiece, Func<Point, int, Point> calculateNextPoint)
        {
            bool captureHasHappened = false;
            int totalFound = CaptureMethod(ogPiece, calculateNextPoint);
            if (totalFound == MaxDistanceForCapture)
            {
                Capture?.Invoke(_board[ogPiece].Color);
                captureHasHappened = true;
                _board.Remove(calculateNextPoint(ogPiece, 1));
                _board.Remove(calculateNextPoint(ogPiece, 2));
            }
            return captureHasHappened;
        }

        private void ProccessGroupings(Func<Point, Point> firstPiece, Func<Point, Point> secondPiece, Point newestPiece)
        {
            int amountFound = CountAllNeighborsFollowingAGivenSequence(firstPiece, newestPiece, 1);
            amountFound = CountAllNeighborsFollowingAGivenSequence(secondPiece, newestPiece, amountFound);
            if (amountFound > 4)
            {
                Win?.Invoke(_board[newestPiece].Color);
            }
            else if (amountFound > 3)
            {
                Tessara?.Invoke(_board[newestPiece].Color);
            }
            else if (amountFound > 2)
            {
                Tria?.Invoke(_board[newestPiece].Color);
            }
        }

        private void CheckVerticalGroupings(Point newestPiece)
        {
            ProccessGroupings((point) => point.AddToY(1), (point) => point.AddToY(-1), newestPiece);
        }

        private void CheckHorizontalGroupings(Point newestPiece)
        {
            ProccessGroupings((point) => point.AddToX(1), (point) => point.AddToX(-1), newestPiece);
        }

        private void CheckDiagonalUpperRightGroupings(Point newestPiece)
        {
            ProccessGroupings((point) => point.AddToX(1).AddToY(1), (point) => point.AddToX(-1).AddToY(-1), newestPiece);
        }

        private void CheckDiagonalUpperLeftGroupings(Point newestPiece)
        {
            ProccessGroupings((point) => point.AddToX(1).AddToY(-1), (point) => point.AddToX(-1).AddToY(1), newestPiece);
        }

        private int CountAllNeighborsFollowingAGivenSequence(Func<Point, Point> calculateNextPoint, Point initialPoint, int InitialAmount)
        {
            // Generate a recursive function with the following delegate in order to dynamically create the next point. 
            int RecursivelyCountNeighbors(Point previousPoint, Point nextPoint, int lastTotalCount)
            {
                int currentlyRunningTotal = lastTotalCount;
                if (CheckIfPointExists(nextPoint) &&
                   _board[nextPoint].Color == _board[previousPoint].Color)
                {
                    currentlyRunningTotal = RecursivelyCountNeighbors(nextPoint,
                                                      calculateNextPoint(nextPoint),
                                                      lastTotalCount + 1);
                }
                return currentlyRunningTotal;
            }

            return RecursivelyCountNeighbors(initialPoint, calculateNextPoint(initialPoint), InitialAmount);
        }

        private bool IsOnBoard(Point placement)
        {
            return ((placement.x >= 0 && placement.x <= Width) && (placement.y >= 0 && placement.y <= Height));
        }

        private bool CheckIfPointExists(Point placement)
        {
            return IsOnBoard(placement) && _board.ContainsKey(placement);
        }

        private bool CheckIfOppositePiece(Point positionDown, PieceColor color, int distanceCounter)
        {
            //checks if the distance is less than the edge of the bracket and makes sure that its color is not the same.
            return (distanceCounter < ClosingBracketDistance && _board[positionDown].Color != color);
        }
        private bool CheckIfJoiningPiece(Point positionDown, PieceColor color, int distanceCounter)
        {
            // checks if it is the end point for a bracket and then if that point has the same color as the one passed in.
            return (distanceCounter == ClosingBracketDistance && _board[positionDown].Color == color);
        }

    }
}

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
            _board = new Dictionary<Point, GamePiece>();
            _captures = new Dictionary<PieceColor, int>();
            //subscribe to our own capture event so we can increment our capture count;
        }

        public int GetTotalCaptures(PieceColor color)
        {
            return _captures[color];
        }

        public bool TakeTurn(Point placement, PieceColor color) //Returns bool result depending on the validity of the position
        {
            bool isValidMove = false;
            if (_board.ContainsKey(placement) || color != CurrentTurn || !IsOnBoard(placement))
            {
                isValidMove = true;
                CoordinateMoves(placement, color);
            }
            return isValidMove;
        }

        private void CoordinateMoves(Point placement, PieceColor color)
        {
            // first check if there is a capture
            // then check if there is a tria
            //if there is a tria check for tessera
            //if there is no tessera fire tria event.
            // if there is a tessera check for win
            // if there is no win fire the tessera event.
            //if there is a win fire the win event and hold it.
        }

        private int CaptureMethod(Point basePoint, Func<Point, int, Point> getNextPoint)
        {
            int distanceCounter = 0;
            Point positionDown;
            do
            {
                positionDown = getNextPoint(basePoint, distanceCounter);

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
                captureHasHappened = true;
                _board.Remove(calculateNextPoint(ogPiece, 1));
                _board.Remove(calculateNextPoint(ogPiece, 2));
            }
            return captureHasHappened;
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

        //some method to check recursively???
        //how would that work??
        //when do we break??
        //if break in pattern???
        // what would be the pattern???
        // we could for the tessera, tria, and win
        // we need to account for when a center piece is placed for the win
        // connect four logic
        // if non-existent || color != ours break out of recursion maybe??
        // non-existent meaning either a null object at point or point not even on board.
        // then check in the opposite but same direction??
        // so in a sense we check down then go up
        // if we checked left then go right
        // if we checked diagonally upper left check downward right
        // if we checked diagonally upper right check downward left

        private bool CheckVerticalTessara()
        {
            bool verticalTesseraIsFound = false;
            //check if there is a tessera down
            //check if there is a tessera up
            return verticalTesseraIsFound;
        }

        private bool CheckHorizontalTessara()
        {
            bool horizontalTesseraIsFound = false;
            //check if there is a tessera left 
            //check if there is a tessera right.
            return horizontalTesseraIsFound;
        }

        private bool CheckVerticalTria()
        {
            bool verticalTriaHasBeenFound = false;
            //check for tria and yeah
            return verticalTriaHasBeenFound;
        }

        private bool CheckHorizontalTria()
        {
            bool horizontalTriaHasBeenFound = false;
            return horizontalTriaHasBeenFound;
        }
        private bool CheckVerticalWin()
        {
            bool verticalWinHasHappened = false;
            return verticalWinHasHappened;
        }

        //maybe we don't need to 
        //check one of the wins after first true?

        private bool CheckHorizontalWin()
        {
            bool horizontalWinHasHappened = false;
            return horizontalWinHasHappened;
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

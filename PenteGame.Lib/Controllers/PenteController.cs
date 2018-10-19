using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using System;
using System.Collections.Generic;

namespace PenteGame.Lib.Controllers
{
    public class PenteController
    {
        //game constants
        private const int MaxDistanceForCapture = 4;
        private const int ClosingBracketDistance = 3;

        //private fields for the actual game
        private readonly IDictionary<Point, GamePiece> _board;
        private readonly IDictionary<PieceColor, int> _captures;
        private int _height;
        private int _width;
        private bool _firstMoveHasBeenMade;
        private bool gameIsOver;
        private PieceColor _currentTurn;

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
        public GameMode CurrentMode { get; set; }
        public PieceColor CurrentTurn
        {
            get => _currentTurn;
            set
            {
                _currentTurn = value;
                TurnChanging?.Invoke(_currentTurn);
            }
        }
        public IEnumerable<GamePiece> Pieces => _board.Values;
        
        public event Action<PieceColor> TurnChanging;
        public event Action<PieceColor> Tessara;
        public event Action<PieceColor> Tria;
        public event Action<PieceColor> Win;
        public event Action<PieceColor> Capture;

        public PenteController()
        {
            _board = new Dictionary<Point, GamePiece>();
            _captures = new Dictionary<PieceColor, int>();
            Width = 19;
            Height = 19;
            CurrentMode = GameMode.SinglePlayer;
            Capture += (color) =>
            {
                _captures[color]++;
                if (_captures[color] >= 5)
                {
                    Win?.Invoke(color);
                }
            };
            Win += (color) => gameIsOver = true;
            ResetGame();
        }

        /// <summary>
        /// resets the current game state to the default
        /// </summary>
        public void ResetGame()
        {
            gameIsOver = false;
            _firstMoveHasBeenMade = false;
            CurrentTurn = PieceColor.Black;
            _captures[PieceColor.Black] = 0;
            _captures[PieceColor.White] = 0;
            _board.Clear();
        }

        /// <summary>
        /// gets the total amount of captures for a given color.
        /// </summary>
        /// <param name="color">the color you want to get the captures for</param>
        /// <returns>the amount of captures for the color</returns>
        public int GetTotalCaptures(PieceColor color)
        {
            int totalCaptures = 0;

            if (_captures.ContainsKey(color))
            {
                totalCaptures = _captures[color];
            }

            return totalCaptures;
        }

        /// <summary>
        /// this method proccess the placement of a piece and color in order to determine if a valid move is made.
        /// if a valid move was made it then proccess the turn. it returns whether or not a valid move was made.
        /// </summary>
        /// <param name="placement">the newest move</param>
        /// <param name="color">color of the piece being placed</param>
        /// <returns>whether or not the turn was valid.</returns>
        public bool TakeTurn(Point placement, PieceColor color)
        {
            bool isValidMove = false;
            if (IsValidMove(placement, color))
            {
                _firstMoveHasBeenMade = true;
                isValidMove = true;
                ProccessTurn(placement, color);
                if (CurrentMode == GameMode.SinglePlayer)
                {
                    RunComputerTurn();
                }

            }
            return isValidMove;
        }

        private void ProccessTurn(Point placement, PieceColor color)
        {
            _board[placement] = new GamePiece(placement, color);
            CoordinateMoves(placement);
            SwitchTurn();

        }

        private bool IsValidMove(Point placement, PieceColor color)
        {
            return !IsPieceAtPoint(placement) &&
                color == CurrentTurn &&
                IsFirstMoveDone(placement, color);
        }

        private bool IsFirstMoveDone(Point placement, PieceColor color)
        {
            return ((!_firstMoveHasBeenMade && CheckIfCenterPoint(placement)) || _firstMoveHasBeenMade);
        }

        private void RunComputerTurn()
        {
            if (!gameIsOver)
            {
                Point placement;
                do
                {
                    placement = GeneratePoint();
                } while (!IsValidMove(placement, PieceColor.White));
                ProccessTurn(placement, PieceColor.White);
            }
        }

        private Random rnJesus = new Random();

        private Point GeneratePoint()
        {
            return new Point()
            {
                x = rnJesus.Next(0, Width + 1),
                y = rnJesus.Next(0, Height + 1)
            };
        }

        /// <summary>
        /// Checks if a given point is in the center of the board.
        /// </summary>
        /// <param name="pointInQuestion">the point in question</param>
        /// <returns></returns>
        private bool CheckIfCenterPoint(Point pointInQuestion)
        {
            Point center = GenerateCenter();
            return center == pointInQuestion;
        }

        public Point GenerateCenter()
        {
            return new Point(Width / 2, Height / 2);
        }

        /// <summary>
        /// Switches the turn.
        /// </summary>
        public void SwitchTurn()
        {
            if (!gameIsOver) CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        }

        /// <summary>
        /// coordinates the moves for the newest piece added.
        /// meaning it goes through the long proccess of calling all the captures and also all the checking for each groupings.
        /// </summary>
        /// <param name="newestPiece">the newest piece added to the collection</param>
        private void CoordinateMoves(Point newestPiece)
        {
            ///check for captures first
            CheckVerticalCapture(newestPiece);
            CheckDiagonalRightCapture(newestPiece);
            CheckDiagonalLeftCapture(newestPiece);
            CheckHorizontalCapture(newestPiece);

            //check for any formations of trias, tesseras or wins.
            CheckHorizontalGroupings(newestPiece);
            CheckVerticalGroupings(newestPiece);
            CheckDiagonalUpperRightGroupings(newestPiece);
            CheckDiagonalUpperLeftGroupings(newestPiece);
        }

        /// <summary>
        /// checks for vertical captures
        /// </summary>
        /// <param name="newestPiece">the newest piece added to the collection</param>
        private void CheckVerticalCapture(Point newestPiece) //Check if a piece could be captured vertically and captures if true
        {
            ProccessCapture(newestPiece, (point, mod) => point.AddToY(mod));
            ProccessCapture(newestPiece, (point, mod) => point.AddToY(-mod));
        }

        /// <summary>
        /// checks for the horizontal captures
        /// </summary>
        /// <param name="newestPiece">the newest piece added to the collection</param>
        private void CheckHorizontalCapture(Point newestPiece)
        {
            ProccessCapture(newestPiece, (point, mod) => point.AddToX(mod));
            ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod));
        }

        /// <summary>
        /// checks for the diagonal right captures
        /// </summary>
        /// <param name="newestPiece">the newest piece added to the collection</param>
        private void CheckDiagonalRightCapture(Point newestPiece)
        {
            ProccessCapture(newestPiece, (point, mod) => point.AddToX(mod).AddToY(mod));
            ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod).AddToY(-mod));
        }

        /// <summary>
        /// Checks for the DiagonalLeft captures
        /// </summary>
        /// <param name="newestPiece">the newest piece added to the collection</param>
        private void CheckDiagonalLeftCapture(Point newestPiece)
        {
            ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod).AddToY(mod));
            ProccessCapture(newestPiece, (point, mod) => point.AddToX(-mod).AddToY(mod));
        }

        /// <summary>
        /// checks for a vertical form of either a tria or tessera or win condition.
        /// it then fires the corresponding event.
        /// </summary>
        /// <param name="newestPiece">the newest addition to the collection.</param>
        private void CheckVerticalGroupings(Point newestPiece)
        {
            ProccessThePatterns((point) => point.AddToY(1), (point) => point.AddToY(-1), newestPiece);
        }

        /// <summary>
        /// checks for a horizontal form of either a tria or tessera or win condition.
        /// it then fires the corresponding event.
        /// </summary>
        /// <param name="newestPiece">the newest addition to the collection.</param>
        private void CheckHorizontalGroupings(Point newestPiece)
        {
            ProccessThePatterns((point) => point.AddToX(1), (point) => point.AddToX(-1), newestPiece);
        }

        /// <summary>
        /// checks for a diagonal upper right form of either a tria or tessera or win condition.
        /// it then fires the corresponding event.
        /// </summary>
        /// <param name="newestPiece">the newest addition to the collection.</param>
        private void CheckDiagonalUpperRightGroupings(Point newestPiece)
        {
            ProccessThePatterns((point) => point.AddToX(1).AddToY(1), (point) => point.AddToX(-1).AddToY(-1), newestPiece);
        }

        /// <summary>
        /// checks for a diagonal upper left form of either a tria or tessera or win condition.
        /// it then fires the respective event for each.
        /// </summary>
        /// <param name="newestPiece">the newest point added to the collection</param>
        private void CheckDiagonalUpperLeftGroupings(Point newestPiece)
        {
            ProccessThePatterns((point) => point.AddToX(1).AddToY(-1), (point) => point.AddToX(-1).AddToY(1), newestPiece);
        }

        /// <summary>
        /// Counts how many points follow the pattern that for the given originPoint
        /// and returns the running total.
        /// </summary>
        /// <param name="originPoint">the origin point to start from</param>
        /// <param name="calculateNextPoint">the method to generate the next point</param>
        /// <returns>the count of pieces that follow the given pattern.</returns>
        private int CountHowManyFollowCapturePattern(Point originPoint, Func<Point, int, Point> calculateNextPoint)
        {
            int distanceCounter = 0;
            Point positionDown;
            do
            {
                positionDown = calculateNextPoint(originPoint, ++distanceCounter);

            } while (distanceCounter < MaxDistanceForCapture &&
                    IsPieceAtPoint(positionDown) &&
                    (CheckIfOppositePiece(positionDown, _board[originPoint].Color, distanceCounter) ||
                    CheckIfJoiningPiece(positionDown, _board[originPoint].Color, distanceCounter)));

            return distanceCounter;
        }

        /// <summary>
        /// Proccess a capture request based on the pattern passed in.
        /// if a capture is found it then fires the capture event.
        /// </summary>
        /// <param name="originPoint">the origin point</param>
        /// <param name="calculateNextPoint">the pattern to generate the next point.</param>
        private void ProccessCapture(Point originPoint, Func<Point, int, Point> calculateNextPoint)
        {
            int totalFound = CountHowManyFollowCapturePattern(originPoint, calculateNextPoint);
            if (totalFound == MaxDistanceForCapture)
            {
                _board.Remove(calculateNextPoint(originPoint, 1));
                _board.Remove(calculateNextPoint(originPoint, 2));
                Capture?.Invoke(_board[originPoint].Color);
            }
        }

        /// <summary>
        /// you call this method with two patterns that make up the full piece of 
        /// what you are proccessing. you then give it the point you want them to use as the point of reference.
        /// it will count the amount of matching pieces and then alert the proper event: tria, tessera, or win.
        /// </summary>
        /// <param name="firstPattern">the first pattern to match.</param>
        /// <param name="secondPattern">the second pattern to match.</param>
        /// <param name="originPoint">the point of reference to work with.</param>
        private void ProccessThePatterns(Func<Point, Point> firstPattern, Func<Point, Point> secondPattern, Point originPoint)
        {
            int amountFound = CountAllNeighborsFollowingAGivenSequence(firstPattern, originPoint, 1);
            amountFound = CountAllNeighborsFollowingAGivenSequence(secondPattern, originPoint, amountFound);
            if (amountFound > 4)
            {
                Win?.Invoke(_board[originPoint].Color);
            }
            else if (amountFound > 3)
            {
                Tessara?.Invoke(_board[originPoint].Color);
            }
            else if (amountFound > 2)
            {
                Tria?.Invoke(_board[originPoint].Color);
            }
        }

        /// <summary>
        /// counts all the neighbors of a point with a given pattern.
        /// you also must set an intial count for how many you wanted.
        /// </summary>
        /// <param name="calculateNextPoint">the pattern that you want the counting to follow</param>
        /// <param name="initialPoint">the starting point</param>
        /// <param name="initialAmount">the inital amount to work with</param>
        /// <returns></returns>
        private int CountAllNeighborsFollowingAGivenSequence(Func<Point, Point> calculateNextPoint, Point initialPoint, int initialAmount)
        {
            int runningTotal = initialAmount;
            Point nextPoint = calculateNextPoint(initialPoint);
            while (IsPieceAtPoint(nextPoint) && _board[nextPoint].Color == _board[initialPoint].Color)
            {
                runningTotal++;
                nextPoint = calculateNextPoint(nextPoint);
            }

            return runningTotal;
        }

        /// <summary>
        /// Checks to see if the given point is even a valid point on our board.
        /// </summary>
        /// <param name="pointInQuestion">point in question.</param>
        /// <returns>returns if the point in question is located within the bounds of the board.</returns>
        private bool IsPointOnBoard(Point pointInQuestion)
        {
            return ((pointInQuestion.x >= 0 && pointInQuestion.x <= Width) && (pointInQuestion.y >= 0 && pointInQuestion.y <= Height));
        }

        /// <summary>
        /// Checks if the point even exists. 
        /// exists is defined as is on the board and there is a piece on that spot.
        /// </summary>
        /// <param name="pointInQuestion">point in question</param>
        /// <returns>whether a piece is actually assigned to the point.</returns>
        private bool IsPieceAtPoint(Point pointInQuestion)
        {
            return IsPointOnBoard(pointInQuestion) && _board.ContainsKey(pointInQuestion);
        }

        /// <summary>
        /// checks if the point in question is a valid canidate basded on position and that 
        /// it is a different color than the target color.
        /// </summary>
        /// <param name="pointInQuestion">the point that in question</param>
        /// <param name="targetColor">the color we don't want it to be</param>
        /// <param name="position">the position of the point</param>
        /// <returns>if the piece is not the target color and is in the correct position.</returns>
        private bool CheckIfOppositePiece(Point pointInQuestion, PieceColor targetColor, int position)
        {
            return (position < ClosingBracketDistance && _board[pointInQuestion].Color != targetColor);
        }

        /// <summary>
        /// This Method Checks if the given Position is an joining piece for a and if the position 
        /// is a valid candidate based on the given position. it also uses the color to see if the position 
        /// shares the same color.
        /// </summary>
        /// <param name="pointInQuestion">piece to check</param>
        /// <param name="targetColor">the color that you would like it to be</param>
        /// <param name="position">the position for the point</param>
        /// <returns>true if the point at the given position is the same as the target color</returns>
        private bool CheckIfJoiningPiece(Point pointInQuestion, PieceColor targetColor, int position)
        {
            return (position == ClosingBracketDistance && _board[pointInQuestion].Color == targetColor);
        }

    }
}

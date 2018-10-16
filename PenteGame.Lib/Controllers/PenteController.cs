using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using System;
using System.Collections.Generic;

namespace PenteGame.Lib.Controllers
{
    public class PenteController
    {
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
        public int GetTotalCaptures(PieceColor color)
        {
            return _captures[color];
        }

        public bool TakeTurn(Point placement, PieceColor color) //Returns bool result depending on the validity of the position
        {
            if (_board.ContainsKey(placement) || color != CurrentTurn || !IsOnBoard(placement))
            {
                return false;
            }
            return true;
        }

        private bool CheckVerticalCapture(Point placement, PieceColor color) //Check if a piece could be captured vertically and captures if true
        {
            int i;
            for (i = 1; i < 4; i++)
            {
                Point positionDown = new Point(placement.x, placement.y - i);
                //TODO: Add logic for positionUp
                if (DoesPieceExist(positionDown)) //Checks if pieces within a range of 4 exists
                {
                    if (i < 3 && color == _board[positionDown].Color)
                    {
                        break;
                    }
                    else if (i == 3 && color != _board[positionDown].Color)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (i == 4)
            {
                Capture(color);
                _board.Remove(new Point(placement.x, placement.y - 1));
                _board.Remove(new Point(placement.x, placement.y - 2));
                return true;
            }
            return false;
        }

        private bool CheckHorizontalCapture()
        {
            return false;
        }

        private bool CheckVerticalTessara()
        {
            return false;
        }

        private bool CheckHorizontalTessara()
        {
            return false;
        }

        private bool CheckVerticalTria()
        {
            return false;
        }

        private bool CheckHorizontalTria()
        {
            return false;
        }
        private bool CheckVerticalWin()
        {
            return false;
        }
        private bool CheckHorizontalWin()
        {
            return false;
        }

        private bool IsOnBoard(Point placement)
        {
            return ((placement.x >= 0 && placement.x <= Width) && (placement.y >= 0 && placement.y <= Height));
        }

        private bool DoesPieceExist(Point placement)
        {
            return IsOnBoard(placement) && _board.ContainsKey(placement);
        }
    }
}

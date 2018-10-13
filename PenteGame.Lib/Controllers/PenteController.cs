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

        public PieceColor CurrentTurn { get; set; }
        public IEnumerable<GamePiece> Pieces { get => _board.Values; }

        public void TakeTurn(Point placement)
        {
        }

        private bool CheckVerticalCapture()
        {
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
    }
}

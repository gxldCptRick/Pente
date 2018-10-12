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

        public void TakeTurn()
        {
        }

        public bool CheckVerticalCapture()
        {
            return false;
        }
        public bool CheckHorizontalCapture()
        {
            return false;
        }
        public bool CheckVerticalTessara()
        {
            return false;
        }
        public bool CheckHorizontalTessara()
        {
            return false;
        }
        public bool CheckVerticalTria()
        {
            return false;
        }
        public bool CheckHorizontalTria()
        {
            return false;
        }
        public bool CheckVerticalWin()
        {
            return false;
        }
        public bool CheckHorizontalWin()
        {
            return false;
        }
    }
}

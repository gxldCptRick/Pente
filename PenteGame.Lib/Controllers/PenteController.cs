using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenteGame.Lib.Controllers
{
    public class PenteController
    {
        public event Action<PieceColor> Tessara;
        public event Action<PieceColor> Tria;
        public event Action<PieceColor> Win;
        public event Action<PieceColor> Capture;
        private IDictionary<Point, GamePiece> _board;
        public void TakeTurn()
        {
        }

        private Boolean CheckVerticalCapture()
        {
            return false;
        }
        private Boolean CheckHorizontalCapture()
        {
            return false;
        }
        private Boolean CheckVerticalTessara()
        {
            return false;
        }
        private Boolean CheckHorizontalTessara()
        {
            return false;
        }
        private Boolean CheckVerticalTria()
        {
            return false;
        }
        private Boolean CheckHorizontalTria()
        {
            return false;
        }
        private Boolean CheckVerticalWin()
        {
            return false;
        }
        private Boolean CheckHorizontalWin()
        {
            return false;
        }
    }
}

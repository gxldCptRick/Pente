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
        private IDictionary<Point, GamePiece> _board;
        public void TakeTurn()
        {
        }

        public Boolean CheckVerticalCapture()
        {
            return false;
        }
        public Boolean CheckHorizontalCapture()
        {
            return false;
        }
        public Boolean CheckVerticalTessara()
        {
            return false;
        }
        public Boolean CheckHorizontalTessara()
        {
            return false;
        }
        public Boolean CheckVerticalTria()
        {
            return false;
        }
        public Boolean CheckHorizontalTria()
        {
            return false;
        }
        public Boolean CheckVerticalWin()
        {
            return false;
        }
        public Boolean CheckHorizontalWin()
        {
            return false;
        }
    }
}

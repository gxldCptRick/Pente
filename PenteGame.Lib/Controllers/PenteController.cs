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
        public Boolean CheckHorizontalTessara()
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

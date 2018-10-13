using Microsoft.VisualStudio.TestTools.UnitTesting;
using PenteGame.Lib.Controllers;
using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using System.Linq;

namespace PenteGame.Lib.Tests.Controllers
{
    /// <summary>
    /// Summary description for PenteControllerShould
    /// </summary>
    [TestClass]
    public class PenteControllerShould
    {
        [TestMethod]
        public void PutTheFirstPieceInTheCenter()
        {
            //arrange
            var thing = new PenteController();
            var point = new Point();
            point.x = 8;
            point.y = 8;
            var piece = PieceColor.Black;
            //act
            var didTheThing = thing.TakeTurn(point, piece);
            //assert
            Assert.IsTrue(didTheThing);
        }

        [TestMethod]
        public void UpdateTheBoardToHaveThePoint()
        {
            //arrange
            var thing = new PenteController();
            var point = new Point();
            point.x = 8;
            point.y = 8;
            var piece = PieceColor.Black;
            //act
            var didTheThing = thing.TakeTurn(point, piece);
            //assert
            Assert.IsTrue(thing.Pieces.Contains(new GamePiece(point, piece)));
        }
    }
}

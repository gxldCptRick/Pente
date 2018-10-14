using Microsoft.VisualStudio.TestTools.UnitTesting;
using PenteGame.Lib.Controllers;
using PenteGame.Lib.Enums;
using PenteGame.Lib.Models;
using System;
using System.Linq;

namespace PenteGame.Lib.Tests.Controllers
{
    /// <summary>
    /// Summary description for PenteControllerShould
    /// </summary>
    [TestClass]
    public class PenteControllerShould
    {
        #region Take Turn Region

        [TestMethod]
        public void Update_The_Board_To_Have_The_Point_Passed_In()
        {
            //arrange
            var game = new PenteController();
            var point = new Point();
            point.x = 8;
            point.y = 8;
            var piece = PieceColor.Black;
            //act
            game.TakeTurn(point, piece);
            var IsOnBoard = game.Pieces.Contains(new GamePiece(point, piece));

            //assert
            Assert.IsTrue(IsOnBoard);
        }


        [TestMethod]
        public void Allow_The_First_Piece_To_Be_In_The_Center_Of_The_Board()
        {
            //arrange
            var game = new PenteController();
            var point = new Point();
            point.x = 8;
            point.y = 8;
            var piece = PieceColor.Black;
            //act
            var IsValidMove = game.TakeTurn(point, piece);
            //assert
            Assert.IsTrue(IsValidMove);
        }

        [TestMethod]
        public void Fail_If_First_Piece_Isnt_In_Center()
        {
            //arrange
            var game = new PenteController();
            var point = new Point();
            point.x = 0;
            point.y = 0;
            var piece = PieceColor.Black;

            //act
            var IsValidMove = game.TakeTurn(point, piece);

            //assert
            Assert.IsFalse(IsValidMove);
        }

        [TestMethod]
        public void Fail_If_First_Player_Isnt_The_Black_Player()
        {
            //arrange
            var game = new PenteController();
            var point = new Point();
            point.x = 8;
            point.y = 8;
            var piece = PieceColor.White;

            //act
            var IsValidMove = game.TakeTurn(point, piece);

            //assert
            Assert.IsFalse(IsValidMove);
        }

        [TestMethod]
        public void Fail_On_Point_That_Is_Negative()
        {
            //arrange
            var game = StartGame();
            var point = new Point();
            point.x = -8;
            point.y = -8;
            var piece = PieceColor.White;

            //act
            var IsValidMove = game.TakeTurn(point, piece);

            //assert
            Assert.IsFalse(IsValidMove);
        }

        [TestMethod]
        public void Change_Colors_After_Turn_Is_Taken()
        {
            //arrange
            var expected = PieceColor.White;
            PieceColor actual;

            //act
            var game = StartGame();
            actual = game.CurrentTurn;

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Fail_On_Point_That_Is_Out_Of_Bounds()
        {
            //arrange
            var game = StartGame();
            var point = new Point();
            point.x = 20;
            point.y = 20;
            var piece = PieceColor.White;
            //act
            var IsValidMove = game.TakeTurn(point, piece);
            //assert
            Assert.IsFalse(IsValidMove);
        }

        [TestMethod]
        public void Accept_Point_That_Is_On_The_Edge_Of_The_Board()
        {
            //arrange
            var game = StartGame();
            var point = new Point();
            point.x = 19;
            point.y = 19;
            var color = PieceColor.White;
            //act
            var isValidMove = game.TakeTurn(point, color);

            //assert
            Assert.IsTrue(isValidMove);
        }

        #endregion

        #region Tria Region
        [TestMethod]
        public void Fire_Tria_Event_When_A_Diagonal_Tria_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4) };
            Point[] whitePoints = { new Point(1, 1), new Point(2, 2), new Point(3, 3) };
            var game = StartGame();
            bool isTriaFound = false;
            game.Tria += (color) => isTriaFound = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(isTriaFound);
        }

        [TestMethod]
        public void Fire_Tria_Event_When_A_Vertical_Tria_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4) };
            Point[] whitePoints = { new Point(1, 1), new Point(1, 2), new Point(1, 3) };
            var game = StartGame();
            bool isTriaFound = false;
            game.Tria += (color) => isTriaFound = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(isTriaFound);
        }

        [TestMethod]
        public void Fire_Tria_Event_When_A_Horizontal_Tria_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4) };
            Point[] whitePoints = { new Point(1, 1), new Point(2, 1), new Point(3, 1) };
            var game = StartGame();
            bool isTriaFound = false;
            game.Tria += (color) => isTriaFound = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(isTriaFound);
        }

        #endregion

        #region Tessera Region
        [TestMethod]
        public void Fire_Tessera_Event_When_A_Horizontal_Tessera_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4), new Point(0, 0) };
            Point[] whitePoints = { new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1) };
            var game = StartGame();
            bool isTesseraFound = false;
            game.Tessara += (color) => isTesseraFound = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(isTesseraFound);
        }

        [TestMethod]
        public void Fire_Tessera_Event_When_A_Vertical_Tessera_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4), new Point(0, 0) };
            Point[] whitePoints = { new Point(1, 1), new Point(1, 2), new Point(1, 3), new Point(1, 4) };
            var game = StartGame();
            bool isTesseraFound = false;
            game.Tessara += (color) => isTesseraFound = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(isTesseraFound);
        }

        [TestMethod]
        public void Fire_Tessera_Event_When_A_Diagonal_Tessera_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4), new Point(0, 0) };
            Point[] whitePoints = { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4) };
            var game = StartGame();
            bool isTesseraFound = false;
            game.Tessara += (color) => isTesseraFound = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(isTesseraFound);
        }

        #endregion

        #region Win Region
        [TestMethod]
        public void Fire_Win_Event_When_A_Diagonal_Win_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4), new Point(0, 0), new Point(0, 1) };
            Point[] whitePoints = { new Point(1, 1), new Point(2, 2), new Point(3, 3), new Point(4, 4), new Point(5, 5) };
            var game = StartGame();
            bool hasWon = false;
            game.Win += (color) => hasWon = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasWon);
        }

        [TestMethod]
        public void Fire_Win_Event_When_A_Horizontal_Win_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4), new Point(0, 0), new Point(0, 1) };
            Point[] whitePoints = { new Point(1, 1), new Point(2, 1), new Point(3, 1), new Point(4, 1), new Point(5, 1) };
            var game = StartGame();
            bool hasWon = false;
            game.Win += (color) => hasWon = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasWon);
        }

        [TestMethod]
        public void Fire_Win_Event_When_A_Vertical_Win_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(2, 3), new Point(8, 3), new Point(8, 4), new Point(0, 0), new Point(0, 1) };
            Point[] whitePoints = { new Point(1, 1), new Point(1, 2), new Point(1, 3), new Point(1, 4), new Point(1, 5) };
            var game = StartGame();
            bool hasWon = false;
            game.Win += (color) => hasWon = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasWon);
        }

        [TestMethod]
        public void Fire_Win_Event_When_Five_Captures_Happen()
        {
            //arrange
            Point[] blackPoints = { new Point(8, 5), new Point(11, 8), new Point(12, 8), new Point(11, 5), new Point(12, 5), new Point(15, 8), new Point(16, 8), new Point(15, 5), new Point(16, 5), new Point(19, 8) };
            Point[] whitePoints = { new Point(9, 8), new Point(10, 8), new Point(9, 5), new Point(10, 5), new Point(13, 8), new Point(14, 8), new Point(13, 5), new Point(14, 5), new Point(17, 8), new Point(18, 8) };
            var game = StartGame();
            bool hasWon = false;
            game.Win += (color) => hasWon = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasWon);
        }

        #endregion

        #region Capture Region
        [TestMethod]
        public void Fire_Capture_Event_When_A_Horizontal_Capture_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(0, 0), new Point(7, 9), new Point(10, 9) };
            Point[] whitePoints = { new Point(8, 9), new Point(9, 9), new Point(8, 10) };
            var game = StartGame();
            bool hasCaptured = false;
            game.Capture += (color) => hasCaptured = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasCaptured);

        }

        [TestMethod]
        public void Fire_Capture_Event_When_A_Vertical_Capture_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(11, 8), new Point(8, 11) };
            Point[] whitePoints = { new Point(8, 9), new Point(8, 10) };
            var game = StartGame();
            bool hasCaptured = false;
            game.Capture += (color) => hasCaptured = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasCaptured);

        }

        [TestMethod]
        public void Fire_Capture_Event_When_A_Diagonal_Capture_Is_Formed()
        {
            //arrange
            Point[] blackPoints = { new Point(11, 8), new Point(11, 11) };
            Point[] whitePoints = { new Point(9, 9), new Point(10, 10) };
            var game = StartGame();
            bool hasCaptured = false;
            game.Capture += (color) => hasCaptured = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsTrue(hasCaptured);

        }

        [TestMethod]
        public void Not_Fire_Capture_Event_When_There_Is_A_False_Diagonal_Capture()
        {
            //arrange
            Point[] blackPoints = { new Point(8, 11), new Point(11, 11), new Point(0, 1) };
            Point[] whitePoints = { new Point(9, 9), new Point(0, 0), new Point(10, 10) };
            var game = StartGame();
            bool hasCaptured = false;
            game.Capture += (color) => hasCaptured = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsFalse(hasCaptured);

        }

        [TestMethod]
        public void Not_Fire_Capture_Event_When_There_Is_A_False_Vertical_Capture()
        {
            //arrange
            Point[] blackPoints = { new Point(8, 11), new Point(11, 11) };
            Point[] whitePoints = { new Point(8, 9), new Point(8, 10) };
            var game = StartGame();
            bool hasCaptured = false;
            game.Capture += (color) => hasCaptured = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsFalse(hasCaptured);

        }

        [TestMethod]
        public void Not_Fire_Capture_Event_When_There_Is_A_False_Horizontal_Capture()
        {
            //arrange
            Point[] blackPoints = { new Point(5, 8), new Point(11, 11) };
            Point[] whitePoints = { new Point(7, 8), new Point(6, 8) };
            var game = StartGame();
            bool hasCaptured = false;
            game.Capture += (color) => hasCaptured = true;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);

            //assert
            Assert.IsFalse(hasCaptured);

        }

        [TestMethod]
        public void Update_Capture_Count_After_A_CaptureIs_Found()
        {
            //arrange
            Point[] blackPoints = { new Point(0, 0), new Point(11, 8) };
            Point[] whitePoints = { new Point(10, 8), new Point(9, 8) };
            var game = StartGame();
            int expected = 1;
            int actual;

            //act
            TakeTurnsWithPoints(game, whitePoints, blackPoints);
            actual = game.GetTotalCaptures(PieceColor.Black);

            //assert
            Assert.AreEqual(expected, actual);

        }

        #endregion

        #region Helper Methods
        private void TakeTurnsWithPoints(PenteController game, Point[] whitePoints, Point[] blackPoints)
        {
            if (whitePoints.Length != blackPoints.Length) throw new ArgumentException("You need to pass in two arrays of equal size");
            int TotalNumber = blackPoints.Length + whitePoints.Length;
            int whitePoint = 0;
            int blackPoint = 0;
            for (int i = 0; i < TotalNumber; i++)
            {
                if (i % 2 == 0)
                {
                    game.TakeTurn(whitePoints[whitePoint++], PieceColor.White);
                }
                else
                {
                    game.TakeTurn(blackPoints[blackPoint++], PieceColor.Black);
                }
            }
        }

        private PenteController StartGame()
        {
            var newGame = new PenteController();
            var point = new Point();
            point.x = 8;
            point.y = 8;
            var color = PieceColor.Black;
            newGame.TakeTurn(point, color);
            return newGame;
        }
        #endregion
    }
}

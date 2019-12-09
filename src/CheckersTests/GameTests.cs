using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        [DataTestMethod]
        [DataRow(Tile.White,1,1, 7)]
        [DataRow(Tile.White,32,6, 0)]
        [DataRow(Tile.White,29, 0, 0)]
        [DataRow(Tile.White,17, 1, 3)]
        public void SetTest(Tile tile,int position, int x, int y)
        {
            var board = Game.Empty();
            board.Set(position, tile);
            Assert.AreEqual(tile, board.Tiles[x,y]);
        }

        [TestMethod()]
        public void Convert_To_White_Queen_After_Move()
        {
            var board = Game.Empty();
            board.Set(5, Tile.White);

            board = board.Move(new Move
            {
                From = Position.FromNumber(5),
                To = Position.FromNumber(1)
            });

            var whitePiece = board.GetTile(Position.FromNumber(1));
            Assert.AreEqual(whitePiece, Tile.QueenWhite);
        }

        [TestMethod()]
        public void Convert_To_White_Queen_After_Eat()
        {
            var board = Game.Empty();
            board.Set(9, Tile.White);
            board.Set(6, Tile.Black);

            board = board.Move(new Eat
            {
                From = Position.FromNumber(9),
                To = Position.FromNumber(2),
                Eaten = new List<Position> { Position.FromNumber(6) }
            }) ;

            var whitePiece = board.GetTile(Position.FromNumber(2));
            Assert.AreEqual(whitePiece, Tile.QueenWhite);
        }
    }
}
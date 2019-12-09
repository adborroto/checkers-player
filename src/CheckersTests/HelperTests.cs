using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Checkers.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [TestMethod()]
        public void Given_A_White_Piece_Should_Move_Right()
        {
            var board = Game.Empty();
            board.Set(29, Tile.White);

            var moves = Helper.CalculateMoves(board, 29);
            var move = moves.First();

            Assert.AreEqual(move.From.Number, 29);
            Assert.AreEqual(move.To.Number, 25);
        }

        [TestMethod()]
        public void Given_A_White_Piece_Should_Move_Left()
        {
            var position = 12;
            var board = Game.Empty();
            board.Set(position, Tile.White);

            var moves = Helper.CalculateMoves(board, position);
            var move = moves.First();

            Assert.AreEqual(move.From.Number, position);
            Assert.AreEqual(move.To.Number, 8);
        }

        [TestMethod()]
        public void Given_A_Black_Piece_Should_Move_Left()
        {
            var position = 21;
            var board = Game.Empty();
            board.Set(position, Tile.Black);

            var moves = Helper.CalculateMoves(board, position);
            var move = moves.First();

            Assert.AreEqual(move.From.Number, position);
            Assert.AreEqual(move.To.Number, 25);
        }

        [TestMethod()]
        public void Given_A_Black_Piece_Should_Move_Right()
        {
            var board = Game.Empty();
            board.Set(1, Tile.Black);

            var moves = Helper.CalculateMoves(board, Position.FromNumber(1));
            var move = moves.First();

            Assert.AreEqual(move.From.Number, 1);
            Assert.AreEqual(move.To.Number, 5);
        }

        [TestMethod()]
        public void Given_A_White_Piece_Should_Eat_Single_Right()
        {
            var board = Game.Empty();
            board.Set(29, Tile.White);
            board.Set(25, Tile.Black);

            var moves = Helper.CalculateMoves(board, Position.FromNumber(29));
            var move = moves.First() as Eat;
            Assert.AreEqual(move.From.Number, 29);
            Assert.AreEqual(move.To.Number, 22);
            Assert.AreEqual(move.Eaten.First().Number, 25);
        }

        [TestMethod()]
        public void Given_A_White_Piece_Should_Eat_Single_Left()
        {
            var board = Game.Empty();
            board.Set(32, Tile.White);
            board.Set(28, Tile.White);
            board.Set(27, Tile.Black);

            var moves = Helper.CalculateMoves(board, Position.FromNumber(32));
            var move = moves.First() as Eat;
            Assert.AreEqual(move.From.Number, 32);
            Assert.AreEqual(move.To.Number, 23);
            Assert.AreEqual(move.Eaten.First().Number, 27);
        }

        [TestMethod()]
        public void Given_A_White_Piece_Should_Eat_Twice()
        {
            var board = Game.Empty();
            board.Set(5, Tile.Black);
            board.Set(9, Tile.White);
            board.Set(17, Tile.White);

            var moves = Helper.CalculateMoves(board, Position.FromNumber(5));
            var move = moves.First() as Eat;
            Assert.AreEqual(move.From.Number, 5);
            Assert.AreEqual(move.To.Number, 21);
            Assert.AreEqual(move.Eaten.ElementAt(0).Number, 9);
            Assert.AreEqual(move.Eaten.ElementAt(1).Number, 17);
        }

        [TestMethod()]
        public void Should_eat_only_once()
        {
            var board = Game.Empty();
            board.Set(9, Tile.Black);
            board.Set(22, Tile.White);
            board.Set(13, Tile.White);
            board.Set(25, Tile.White);
            board.Set(14, Tile.White);

            var moves = Helper.CalculateMoves(board, Position.FromNumber(9));
            var move = moves.First() as Eat;
            Assert.AreEqual(move.From.Number, 9);
            Assert.AreEqual(move.To.Number, 18);
        }

    }
}
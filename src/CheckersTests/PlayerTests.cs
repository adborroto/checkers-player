using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void Should_Eat_Better_Than_Move()
        {
            var game = Game.Empty();
            game.Set(30, Tile.White);
            game.Set(25, Tile.Black);

            var move = Player.NextBestMove(game, Tile.White, 1);
            Assert.IsTrue(move is Eat);
        }

        [TestMethod()]
        public void Should_Wait_And_Then_Eat()
        {
            var game = Game.Empty();
            game.Set(29, Tile.White);
            game.Set(20, Tile.White);
            game.Set(21, Tile.Black);

            var move = Player.NextBestMove(game, Tile.White, 2);
            Assert.AreEqual(move.To.Number, 16); // Should wait
        }

        [TestMethod()]
        public void Should_eat_oblicatory()
        {
            var game = Game.Empty();
            game.Set(6, Tile.White);
            game.Set(10, Tile.Black);

            var move = Player.NextBestMove(game, Tile.White, 2);
            Assert.IsTrue(move is Eat);
        }

        [TestMethod()]
        public void Should_not_move_defence()
        {
            var game = Game.Empty();
            game.Set(30, Tile.White);
            game.Set(27, Tile.White);

            var move = Player.NextBestMove(game, Tile.White, 2);
            Assert.AreEqual(move.From.Number,27);
        }
    }
}
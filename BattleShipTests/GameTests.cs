using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BattleShip;
using BattleShip.MainComponents.GameBoardLogic.BaseBattleOperations;

namespace BattleShipTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void IsGameOverTest()
        {
            var game = new Game();
            GameBoardUtilis.BackToMenu = 1;

            Assert.IsTrue(game.IsGameOver());

            GameBoardUtilis.BackToMenu = 0;
            game.Board.Rockets = 0;

            Assert.IsTrue(game.IsGameOver());

            game.Board.Rockets = 666;
            game.AllBoatsSunk(game.Board);

            Assert.IsFalse(game.IsGameOver());
        }

        [TestMethod]
        public void AllBoatsSunkTest()
        {
            var game = new Game();

            Assert.IsFalse(game.AllBoatsSunk(game.Board));
        }
    }
}

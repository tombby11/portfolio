using DiceInvader.Base.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiceInvader.Base.Models;
using Moq;
namespace DiceInvader.Base.Tests
{
    [TestClass]
    public class GameModelTests
    {
        [TestMethod]
        public void EndGame_Should_set_Gameover_True()
        {
            //Arrange
            var sut = new GameModel();
            //Act
            sut.EndGame();
            //Assert
            Assert.AreEqual(sut.Score,0);
            Assert.IsTrue(sut.GameOver);
        }

        [TestMethod]
        public void InitializeGameStatusTest()
        {
            var sut = new GameModel();
            sut.InitializeGameStatus();

            Assert.AreEqual(sut.Score, 0);
            Assert.IsTrue(sut.Lives>0);
            Assert.IsTrue(sut.Wave == 0);
            Assert.IsFalse(sut.GameOver);
        }

        [TestMethod]
        public void RocketShotTest()
        {
            //Arrange 
            var sut = new GameModel {GameOver = false};
            var startingPoint = new Helpers.Point(12,20);
            //Execute
            sut.RocketShot(startingPoint);
            //Assert
            Assert.IsTrue(sut.PlayerShots.Count > 0);
        }


        [TestMethod]
        public void MovePlayerTest()
        {
            //Arrange 
            var initialPlayerPoint = new Point(10.10, 20.20);
            var player = new Player(initialPlayerPoint);
            var sut = new GameModel(player) { GameOver = false };
          
            //Execute
            sut.MovePlayer(Direction.Right);
            //Assert
            Assert.AreNotEqual(player.Location.X , initialPlayerPoint.X);         
        }

        

    }
}

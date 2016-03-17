﻿using System.Collections.Generic;
using DiceInvader.Base.Helpers;
using DiceInvader.Base.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var gameModelHelperMock = new Mock<GameModelHelper>();
            var sut = new GameModel(gameModelHelperMock.Object);
            //Act
            sut.EndGame();
            //Assert
            Assert.AreEqual(sut.Score, 0);
            Assert.IsTrue(sut.GameOver);
        }

        [TestMethod]
        public void InitializeGameStatusTest()
        {
            var gameModelHelperMock = new Mock<GameModelHelper>();
            var sut = new GameModel(gameModelHelperMock.Object);
            sut.InitializeGameStatus();

            Assert.AreEqual(sut.Score, 0);
            Assert.IsTrue(sut.Lives > 0);
            Assert.IsTrue(sut.Wave == 0);
            Assert.IsFalse(sut.GameOver);
            Assert.IsTrue(sut.PlayerShots.Count == 0);
            Assert.IsTrue(sut.InvaderShots.Count == 0);
            Assert.IsTrue(sut.Invaders.Count == 0);
        }

        [TestMethod]
        public void RocketShotTest()
        {
            //Arrange 
            var gameModelHelperMock = new Mock<GameModelHelper>();
            var sut = new GameModel(gameModelHelperMock.Object) {GameOver = false};
        
            var startingPoint = new Point(12, 20);
            //Execute
            sut.RocketShot(startingPoint);
            //Assert
            Assert.IsTrue(sut.PlayerShots.Count > 0);
        }

        [TestMethod]
        public void BombShotTest()
        {
            //Arrange 
            var gameModelHelperMock = new Mock<GameModelHelper>();
            gameModelHelperMock.Setup(m => m.CanFireBomb(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            var sut = new GameModel(gameModelHelperMock.Object) { GameOver = false };
            sut.NextWave();
            var startingPoint = new Point(12, 20);
         

            //Execute
            sut.FireBomb(startingPoint);
            //Assert
            Assert.IsTrue(sut.InvaderShots.Count > 0);
        }

        [TestMethod]
        public void InitializePlayerTest()
        {
            //Arrange 
            var gameModelHelperMock = new Mock<GameModelHelper>();
            var initialPlayerPoint = new Point(10.10, 20.20);
            var player = new Player(initialPlayerPoint);
            var sut = new GameModel(gameModelHelperMock.Object, player) { GameOver = false };

            //Execute
            sut.InitializePlayerShip();

            //Assert
            Assert.IsNotNull(player.Location);
        }

        [TestMethod]
        public void MovePlayerTest()
        {
            //Arrange 
            var gameModelHelperMock = new Mock<GameModelHelper>();
            var initialPlayerPoint = new Point(10.10, 20.20);
            var player = new Player(initialPlayerPoint);
            var sut = new GameModel(gameModelHelperMock.Object, player) {GameOver = false};

            //Execute
            sut.MovePlayer(Direction.Right);
            //Assert
            Assert.AreNotEqual(player.Location.X, initialPlayerPoint.X);
        }
      
    }
}
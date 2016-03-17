using System;
using System.Collections.Generic;
using System.Linq;
using DiceInvader.Base.Helpers;

namespace DiceInvader.Base.Models
{
    public class GameModelHelper : IGameModelHelper
    {
        private readonly Random _random;
        private bool _justMovedDown;
        private Direction _invadersDirection; 
        public GameModelHelper()
        {
            _random = new Random();
        }

        public bool CanFireBomb(int count, int wave)
        {
            if (count > wave + 1)
                return false;

            if (_random.Next(10) < 10 - wave)
                return false;

            return true;
        }

        public Point GetRandomBombLocation(List<Invader> invaders)
        {
            var result =
                from invader in invaders
                group invader by invader.Location.X
                into invaderGroup
                orderby invaderGroup.Key descending
                select invaderGroup;

            var randomGroup = result.ElementAt(_random.Next(result.ToList().Count));
            var bottomInvader = randomGroup.Last();

            var shotLocation = new Point(bottomInvader.Location.X + bottomInvader.Area.Width/2,
                bottomInvader.Location.Y + 2);
            return shotLocation;
        }

        public bool CanInvadorsMove(int wave, int invadersCount, DateTime lastUpdated)
        {
            double millisecondsBetweenMovements = Math.Min(10 - wave, 1) * 2 * invadersCount;
            var result = DateTime.Now - lastUpdated <= TimeSpan.FromMilliseconds(millisecondsBetweenMovements*10);
            return !result; 
        }

        public Direction GetInvadersDirection(List<Invader> invaders, Size playAreaSize)
        {
            var invadersTouchingLeftBoundary = (from invader in invaders
                                               where invader.Area.Left < Invader.HorizontalInterval
                                               select  invader).ToList();

            var invadersTouchingRightBoundary = (from invader in invaders
                                                where invader.Area.Right > playAreaSize.Width - Invader.HorizontalInterval * 2
                                                select invader).ToList();
            if (_justMovedDown)
            {
                _justMovedDown = false;
                if (invadersTouchingLeftBoundary.Any())
                {
                    _invadersDirection = Direction.Right;
                    return Direction.Right;
                 }
                if (invadersTouchingRightBoundary.Any())
                {
                    _invadersDirection = Direction.Left;
                    return Direction.Left;
                }
            }

            if (invadersTouchingLeftBoundary.Any() || invadersTouchingRightBoundary.Any())
            {
                _justMovedDown = true;
                _invadersDirection = Direction.Down;
                return Direction.Down;
            }

            return _invadersDirection;        

        }
    }
}
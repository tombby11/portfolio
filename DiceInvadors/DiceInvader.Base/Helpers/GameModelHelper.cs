using System;
using System.Collections.Generic;
using System.Linq;
using DiceInvader.Base.Models;

namespace DiceInvader.Base.Helpers
{
    public class GameModelHelper : IGameModelHelper
    {
        private readonly Random _random;

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

        public bool CanInvadersMove(int wave, int invadersCount, DateTime lastUpdated)
        {
            double millisecondsBetweenMovements = Math.Min(10 - wave, 1) * 2 * invadersCount;
            var result = DateTime.Now - lastUpdated <= TimeSpan.FromMilliseconds(millisecondsBetweenMovements);
            return !result; 
        }

        public Direction GetDirectionToMoveInvaders(Direction lastInvaderDirection, double playAreaWidth, List<Invader> invaders)
        {
            var invadersTouchingLeftBoundary = from invader in invaders
                                               where invader.Area.Left < Invader.HorizontalInterval
                                               select invader;
            var invadersTouchingRightBoundary = from invader in invaders
                                                where invader.Area.Right > playAreaWidth - Invader.HorizontalInterval * 2
                                                select invader;

            if (invadersTouchingRightBoundary.Any() || invadersTouchingLeftBoundary.Any())
            {
                if (lastInvaderDirection == Direction.Down)
                {
                    if (invadersTouchingLeftBoundary.Any())
                    {
                        return Direction.Right;
                    }
                    else if (invadersTouchingRightBoundary.Any())
                    {
                        return Direction.Left;
                    }

                }
                else
                {
                    return Direction.Down;
                }
            }
            return lastInvaderDirection;
        }
    }
}
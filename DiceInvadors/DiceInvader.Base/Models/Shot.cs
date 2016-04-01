using DiceInvader.Base.Helpers;
using System;

namespace DiceInvader.Base.Models
{
    public class Shot
    {
        public const double ShotPixelsPerSecond = 95;
        private DateTime _lastMoved;

        public static Size ShotSize = new Size(2, 10);
        public Point Location { get; private set; }
        public Direction Direction { get; private set; }
        public ShotType ShotType { get; private set; }

        public Shot(Point location, Direction direction,ShotType shotType)
        {
            Location = location;
            Direction = direction;
            _lastMoved = DateTime.Now;
            ShotType = shotType;
        }

        public void Move()
        {
            var timeSinceLastMoved = DateTime.Now - _lastMoved;
            var distance = timeSinceLastMoved.Milliseconds * ShotPixelsPerSecond / 1000;
            if (Direction == Direction.Up)
            {
                // moving up means moving backwards 
                distance = distance * -1;
            }
            Location = new Point(Location.X, Location.Y + distance);
            _lastMoved = DateTime.Now;
        }

    }
}

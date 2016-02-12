using DiceInvader.Base.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceInvader.Base.Models
{
    public abstract class Ship
    {
        public Point Location { get;  set; }
        public Size Size { get; private set; }

        public Rect Area
        {
            get
            {
                return new Rect(Location, Size);
            }
        }

        public Ship(Size size, Point location)
        {
            Location = location;
            Size = size;
        }
        public void SetLocation(Point location)
        {
            Location = location;
        }

        public abstract void Move(Direction direction);
    }
}

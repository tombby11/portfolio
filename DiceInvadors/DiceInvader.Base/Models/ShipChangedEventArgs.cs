using System;
namespace DiceInvader.Base.Models
{
    public class ShipChangedEventArgs:EventArgs
    {
        public Ship ShipUpdated { get; private set; }
        public bool Killed { get; private set; }

        public ShipChangedEventArgs(Ship shipUpdated, bool killed)
        {
            ShipUpdated = shipUpdated;
            Killed = killed;
        }
    }
}

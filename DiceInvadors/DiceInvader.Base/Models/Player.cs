using DiceInvader.Base.Helpers;

namespace DiceInvader.Base.Models
{
    public class Player : Ship
    {
        public static readonly Size PlayerSize = new Size(25, 15);
        const double PixelsToMove = 10;

        public Player(Point location) : base(PlayerSize,location)
        {
           
        }

        public override void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Location = new Point(Location.X - PixelsToMove, Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point(Location.X + PixelsToMove, Location.Y);
                    break;
                default:
                    break;
            }
        }
    }
}

using DiceInvader.Base.Helpers;

namespace DiceInvader.Base.Models
{
    public class Invader : Ship
    {
        public static readonly Size InvaderSize = new Size(15, 15);
        public const int HorizontalInterval = 5; 
        public const int VerticalInterval = 15; 

        public InvaderType InvaderType { get; private set; }
        public int Score { get; private set; }

        public Invader(InvaderType invaderType, Point location, int score) : base(InvaderSize,location)
        {          
            Score = score;
            InvaderType = invaderType;
        }

        /// <summary>
        /// Mode the invader
        /// </summary>
        /// <param name="direction">The direction of movemen: Left, Right, Down</param>
        public override void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    Location = new Point(Location.X - HorizontalInterval, Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point(Location.X + HorizontalInterval, Location.Y);
                    break;
                default:
                    Location = new Point(Location.X, Location.Y + VerticalInterval);
                    break;
            }
        }
    }
}

using Microsoft.Xna.Framework;

namespace BSDRobotsConsole.Windows.Events
{
    public class PlayerMoveEventArgs
    {
        public PlayerMoveEventArgs(Point direction)
        {
            Direction = direction;
        }

        public Point Direction { get; private set; }
    }
}

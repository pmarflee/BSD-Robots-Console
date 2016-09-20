using BSDRobotsConsole.Windows.Consoles;
using Microsoft.Xna.Framework;

namespace BSDRobotsConsole.Windows.GameObjects
{
    public class Player : Entity
    {
        public Player() : base(Color.Yellow, 1)
        {
        }

        public int Teleports { get; set; }
    }
}

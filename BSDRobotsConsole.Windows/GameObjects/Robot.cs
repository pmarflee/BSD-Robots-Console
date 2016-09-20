using Microsoft.Xna.Framework;

namespace BSDRobotsConsole.Windows.GameObjects
{
    public class Robot : Entity
    {
        public Robot() : base(Color.Red, 12)
        {
        }

        protected override void KillInternal()
        {
            var frame = Animation.CurrentFrame[0];
            frame.GlyphIndex = 15;
            frame.Foreground = Color.Purple;

            base.KillInternal();
        }

        public void MoveTowardsPlayer(Player player)
        {
            var newPosition = Position;

            if (Position.X > player.Position.X) newPosition.X -= 1;
            if (Position.X < player.Position.X) newPosition.X += 1;
            if (Position.Y > player.Position.Y) newPosition.Y -= 1;
            if (Position.Y < player.Position.Y) newPosition.Y += 1;

            Position = newPosition;
        }
    }
}

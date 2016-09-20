using Microsoft.Xna.Framework;
using System;
using SadConsole.Input;
using Microsoft.Xna.Framework.Input;
using BSDRobotsConsole.Windows.Events;

namespace BSDRobotsConsole.Windows.Consoles
{
    public class MapConsole : BorderedConsole
    {
        public event EventHandler<PlayerMoveEventArgs> PlayerMoveEvent;
        public event EventHandler PlayerTeleportEvent;
        public event EventHandler NewGameEvent;

        private Random _random = new Random();
        private readonly RobotsGame _game;

        public MapConsole(RobotsGame game, int width, int height) : base(width, height)
        {
            _game = game;

            // Keyboard setup
            SadConsole.Engine.Keyboard.RepeatDelay = 0.07f;
        }

        public override void Render()
        {
            base.Render();

            foreach (var entity in _game.Entities)
            {
                entity.Render();
            }
        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {

            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.N)))
            {
                NewGameEvent?.Invoke(this, new EventArgs());
                return true;
            }

            if (!_game.Player.IsAlive) return false;

            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.T)))
            {
                PlayerTeleportEvent?.Invoke(this, new EventArgs());
                return true;
            }

            var keyHit = false;
            var direction = new Point();

            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Up)) 
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad8))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad7))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad9)))
            {
                direction.Y = -1;
                keyHit = true;
            }
            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Down)) 
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad2))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad1))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad3)))
            {
                direction.Y += 1;
                keyHit = true;
            }
            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Left)) 
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad4))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad7))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad1)))
            {
                direction.X -= 1;
                keyHit = true;
            }
            if (info.KeysPressed.Contains(AsciiKey.Get(Keys.Right)) 
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad6))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad9))
                || info.KeysPressed.Contains(AsciiKey.Get(Keys.NumPad3)))
            {
                direction.X += 1;
                keyHit = true;
            }

            if (keyHit)
            {
                PlayerMoveEvent?.Invoke(this, new PlayerMoveEventArgs(direction));
            }

            return keyHit;
        }

        public void PrintGameOver()
        {
            Print((Width / 2) - 5, Height / 2, "GAME OVER");
        }

        public void PrintNewGame()
        {
            Print((Width / 2) - 5, Height / 2, "         ");
        }
    }
}

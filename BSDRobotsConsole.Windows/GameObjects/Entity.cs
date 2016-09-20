using SadConsole.Game;
using Microsoft.Xna.Framework;
using SadConsole.Consoles;
using BSDRobotsConsole.Windows.Consoles;
using System;

namespace BSDRobotsConsole.Windows.GameObjects
{
    public abstract class Entity : GameObject
    {
        protected Entity(Color foregroundColour, int glyphIndex) : base(SadConsole.Engine.DefaultFont)
        {
            var anim = new AnimatedTextSurface("default", 1, 1, SadConsole.Engine.DefaultFont);
            var frame = anim.CreateFrame();
            frame[0].Foreground = foregroundColour;
            frame[0].Background = Color.Transparent;
            frame[0].GlyphIndex = glyphIndex;

            Animation = anim;
            anim.Start();

            IsAlive = true;
        }

        public bool IsAlive { get; private set; }

        public void Kill()
        {
            IsAlive = false;

            KillInternal();
        }

        protected virtual void KillInternal()
        {

        }
    }
}

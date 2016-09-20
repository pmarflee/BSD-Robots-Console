namespace BSDRobotsConsole.Windows.Consoles
{
    public abstract class BorderedConsole : SadConsole.Consoles.Console
    {
        protected BorderedConsole(int width, int height) : base(width, height)
        {
            var box = SadConsole.Shapes.Box.GetDefaultBox();
            box.Width = width;
            box.Height = height;
            box.Draw(this);
        }
    }
}

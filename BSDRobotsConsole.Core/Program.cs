using BSDRobotsConsole.Windows;

namespace BSDRobotsConsole.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new RobotsGame())
                game.Run();
        }
    }
}

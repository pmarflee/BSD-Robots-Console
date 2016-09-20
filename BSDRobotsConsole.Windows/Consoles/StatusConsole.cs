namespace BSDRobotsConsole.Windows.Consoles
{
    public class StatusConsole : BorderedConsole
    {
        public StatusConsole(int width, int height) : base(width, height)
        {
            Print(2, 1, "Directions:");
            Print(2, 3, "7 8 9");
            Print(2, 4, " \\|/");
            Print(2, 5, "4- -6");
            Print(2, 6, " /|\\");
            Print(2, 7, "1 2 3");

            Print(2, 9, "Commands:");
            Print(2, 10, "t: teleport");

            Print(2, 13, "Legend:");
            Print(2, 15, "[c:r f:red][c:sg 12] [c:r f:default]: robot");
            Print(2, 16, "[c:r f:purple][c:sg 15] [c:r f:default]: junk heap");
            Print(2, 17, "[c:r f:yellow][c:sg 1] [c:r f:default]: you");
        }

        public void PrintLevel(int level)
        {
            Print(2, 19, $"Level: {level}");
        }

        public void PrintRobots(int robots)
        {
            Print(2, 20, $"Robots: {robots}");
        }

        public void PrintScore(int score)
        {
            Print(2, 21, $"Score: {score}");
        }

        public void PrintTeleports(int teleports)
        {
            Print(2, 22, $"Teleports: {teleports}");
        }
    }
}

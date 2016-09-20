using BSDRobotsConsole.Windows.Consoles;
using BSDRobotsConsole.Windows.GameObjects;
using Microsoft.Xna.Framework;
using SadConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSDRobotsConsole.Windows
{
    public class RobotsGame : Game
    {
        GraphicsDeviceManager _graphics;

        List<Entity> _entities;
        Player _player;

        MapConsole _map;
        StatusConsole _status;

        int _level = 0;
        int _score = 0;
        
        readonly Random _random = new Random();

        public RobotsGame() : base()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.Title = "BSD Robots";
        }

        public IEnumerable<Entity> Entities
        {
            get { return _entities; }
        }

        public IEnumerable<Robot> Robots
        {
            get { return _entities.OfType<Robot>(); }
        }

        public Player Player
        {
            get { return _player; }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Engine.Initialize(_graphics, "IBM.font", 100, 34);
            Engine.UseMouse = true;
            Engine.UseKeyboard = true;

            _map = new MapConsole(this, 80, 34);
            _map.PlayerTeleportEvent += PlayerTeleportEvent;
            _map.PlayerMoveEvent += PlayerMoveEvent;
            _map.NewGameEvent += NewGameEvent;

            _status = new StatusConsole(20, 34);
            _status.Position = new Point(80, 0);

            Engine.ConsoleRenderStack = new SadConsole.Consoles.ConsoleList { _map, _status };
            Engine.ActiveConsole = Engine.ConsoleRenderStack[0];

            NextLevel();

            base.Initialize();
        }

        private void NewGameEvent(object sender, EventArgs e)
        {
            _level = 0;
            _score = 0;

            _map.PrintNewGame();

            NextLevel();
        }

        private void PlayerMoveEvent(object sender, Events.PlayerMoveEventArgs e)
        {
            var newPosition = _player.Position += e.Direction;

            if (!IsWalkable(newPosition)) return;

            _player.Position = newPosition;

            UpdateGameState();
        }

        private void PlayerTeleportEvent(object sender, EventArgs e)
        {
            if (_player.Teleports == 0) return;

            Point newPosition;

            do
            {
                newPosition = GenerateRandomPosition();
            } while (newPosition == _player.Position);

            _player.Position = newPosition;
            _player.Teleports--;

            UpdateGameState();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime, IsActive);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Clear the screen with black, like a traditional console.
            GraphicsDevice.Clear(Color.Black);

            // Draw the consoles to the screen.
            Engine.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void NextLevel()
        {
            _level++;

            _entities = new List<Entity>();

            _player = new Player();
            _player.Position = new Point(_map.Width / 2, _map.Height / 2);
            _player.RenderOffset = _map.Position;
            _player.Teleports = ((_level - 1) * 2) + 3;

            _entities.Add(_player);

            var robots = ((_level - 1) * 10) + 5;

            for (var i = 0; i < robots; i++)
            {
                GenerateRobot();
            }

            UpdateStatus();
        }

        private void GenerateRobot()
        {
            Point position;

            var robot = new Robot();

            do
            {
                position = GenerateRandomPosition();
            }
            while (_entities.Any(e => e.Position == position)
                    || Math.Abs(position.X - _player.Position.X) < 5
                    || Math.Abs(position.Y - _player.Position.Y) < 5);

            robot.Position = position;
            robot.RenderOffset = _map.Position;
            _entities.Add(robot);
        }

        private void UpdateStatus()
        {
            _status.PrintLevel(_level);
            _status.PrintRobots(Robots.Count());
            _status.PrintScore(_score);
            _status.PrintTeleports(_player.Teleports);
        }

        private bool IsWalkable(Point position)
        {
            return position.X > 0 && position.X < _map.Width - 1 
                && position.Y > 0 && position.Y < _map.Height - 1;
        }

        private Point GenerateRandomPosition()
        {
            return new Point(_random.Next(1, _map.Width - 1), _random.Next(1, _map.Height - 1));
        }

        private void UpdateGameState()
        {
            MoveRobots();
            CollisionDetection();
            UpdateStatus();

            if (!_player.IsAlive)
            {
                _map.PrintGameOver();
            }
            else if (Robots.All(robot => !robot.IsAlive))
            {
                NextLevel();
            }
        }

        private void MoveRobots()
        {
            foreach (var robot in Robots.Where(robot => robot.IsAlive))
            {
                robot.MoveTowardsPlayer(_player);
            }
        }

        private void CollisionDetection()
        {
            foreach (var robot in Robots)
            {
                var isAlive = robot.IsAlive;

                foreach (var otherRobot in Robots)
                {
                    if (robot == otherRobot) continue;

                    if (robot.Position == otherRobot.Position)
                    {
                        if (robot.IsAlive)
                        {
                            robot.Kill();
                            _score++;
                        }

                        if (otherRobot.IsAlive)
                        {
                            otherRobot.Kill();
                            _score++;
                        }
                    }
                }

                if (robot.Position == _player.Position)
                {
                    _player.Kill();
                }
            }
        }
    }
}

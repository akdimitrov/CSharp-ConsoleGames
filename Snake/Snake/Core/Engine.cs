using System;
using System.Threading;
using SnakeGame.Enums;
using SnakeGame.GameObjects;

namespace SnakeGame.Core
{
    public class Engine : IEngine
    {
        private readonly Wall wall;
        private readonly Snake snake;
        private readonly Point[] pointsOfDirection;
        private double sleepTime;
        private Direction direction;
        private readonly int leftX;
        private readonly int topY;

        public Engine(Wall wall, Snake snake)
        {
            this.wall = wall;
            this.snake = snake;
            this.sleepTime = 100;
            this.pointsOfDirection = new Point[4];
            this.leftX = 2;
            this.topY = this.wall.TopY + 1;
        }

        public void Run()
        {
            this.CreateDirections();
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    GetNextDirection();
                }

                bool isMoving = snake.IsMoving(this.pointsOfDirection[(int)direction]);

                if (!isMoving)
                {
                    AskUserForRestart();
                }

                sleepTime -= 0.01;
                Thread.Sleep((int)sleepTime);

                Console.SetCursorPosition(this.leftX, this.topY);
                Console.WriteLine($"Your score is: {snake.Score}");
            }
        }

        private void CreateDirections()
        {
            this.pointsOfDirection[0] = new Point(1, 0);
            this.pointsOfDirection[1] = new Point(-1, 0);
            this.pointsOfDirection[2] = new Point(0, 1);
            this.pointsOfDirection[3] = new Point(0, -1);
        }

        private void GetNextDirection()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            if (userInput.Key == ConsoleKey.LeftArrow)
            {
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
            }
            else if (userInput.Key == ConsoleKey.RightArrow)
            {
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
            else if (userInput.Key == ConsoleKey.UpArrow)
            {
                if (direction != Direction.Down)
                {
                    direction = Direction.Up;
                }
            }
            else if (userInput.Key == ConsoleKey.DownArrow)
            {
                if (direction != Direction.Up)
                {
                    direction = Direction.Down;
                }
            }
            else if (userInput.Key == ConsoleKey.Escape)
            {
                GameOver();
                AskUserForRestart();
            }

            Console.CursorVisible = false;
        }

        private void AskUserForRestart()
        {
            GameOver();
            Console.SetCursorPosition(this.wall.LeftX / 2 - 5, this.topY);
            Console.Write("Would you like to continue? y/n ");

            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                Console.Clear();
                StartUp.Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void GameOver()
        {
            Console.SetCursorPosition(this.wall.LeftX / 2 - 8, this.topY / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("*** GAME OVER ***");
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}

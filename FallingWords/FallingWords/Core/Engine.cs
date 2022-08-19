using System;
using System.Threading;
using FallingWords.GameObjects;

namespace FallingWords.Core
{
    public class Engine : IEngine
    {
        private const int SleepTime = 40;
        private const int FramesToMove = 22;
        private int frame = 0;
        private int seconds = 60;

        private readonly Field field;
        private readonly WordPool wordPool;

        public Engine(Field field, WordPool wordPool)
        {
            this.field = field;
            this.wordPool = wordPool;
        }

        public void Run()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        PrintGameOver();
                        AskForRestart();
                    }

                    wordPool.ProccessInput(key.KeyChar);
                }

                if (frame == FramesToMove)
                {
                    seconds--;
                    frame = 0;

                    if (!wordPool.IsMoving())
                    {
                        PrintGameOver();
                        AskForRestart();
                    }
                }

                frame++;
                PrintTime();
                PrintStats();
                Thread.Sleep(SleepTime);
            }
        }

        private void PrintStats()
        {
            Write($"Score: {wordPool.Score}", 3, 1);
            Write($"Words: {wordPool.CorrectWords}", field.LeftX - 12, 1);
        }

        private void PrintTime()
        {
            string time = string.Empty;
            if (seconds >= 60)
            {
                time = $"{seconds / 60}:{seconds % 60:d2}";
            }
            else if (seconds > 4)
            {
                time = $"0:{seconds:d2}";
            }
            else
            {
                Console.ForegroundColor = seconds % 2 == 0 ? ConsoleColor.Red : ConsoleColor.White;
                time = $"0:{seconds:d2}";
            }

            Write(time, field.LeftX / 2 - 2, 1);
            Console.ForegroundColor = ConsoleColor.White;

            if (seconds == 0)
            {
                PrintWellDone();
                AskForRestart();
            }
        }

        private void AskForRestart()
        {
            Write("Would you like to continue? (y/n) ", 2, 3);
            string input = Console.ReadLine();
            if (input.ToLower() == "y")
            {
                Console.Clear();
                Console.ResetColor();
                StartUp.Main();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void PrintWellDone()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            int row = field.TopY / 2 - 1;
            int col = field.LeftX / 2 - 6;
            Write("╔═════════╗", col, row);
            Write("║ Well    ║", col, row + 1);
            Write("║   done! ║", col, row + 2);
            Write("╚═════════╝", col, row + 3);
        }

        private void PrintGameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int row = field.TopY / 2 - 1;
            int col = field.LeftX / 2 - 6;
            Write("╔═════════╗", col, row);
            Write("║ Game    ║", col, row + 1);
            Write("║   over! ║", col, row + 2);
            Write("╚═════════╝", col, row + 3);
        }

        private void Write(string text, int leftX, int topY)
        {
            Console.SetCursorPosition(leftX, topY);
            Console.Write(text);
        }
    }
}

using System;
using System.Threading;

namespace FallingWords
{
    public class Engine
    {
        private const int SleepTime = 40;
        private const int FramesToMove = 22;
        private int frame = 0;
        private int seconds = 60;

        private readonly Field field;
        private readonly WordPool wordPool;

        public Engine(Field field, Level level)
        {
            this.field = field;
            wordPool = new WordPool(field, level);
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
            Console.SetCursorPosition(3, 1);
            Console.Write($"Score: {wordPool.Score}");
            Console.SetCursorPosition(field.LeftX - 12, 1);
            Console.Write($"Words: {wordPool.CorrectWords}");
        }

        private void PrintTime()
        {
            Console.SetCursorPosition(field.LeftX / 2 - 2, 1);
            if (seconds >= 60)
            {
                Console.Write($"{seconds / 60}:{seconds % 60:d2}");
            }
            else if (seconds > 4)
            {
                Console.Write($"0:{seconds:d2}");
            }
            else
            {
                Console.ForegroundColor = seconds % 2 == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write($"0:{seconds:d2}");
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (seconds == 0)
            {
                PrintWellDone();
                AskForRestart();
            }
        }

        private void AskForRestart()
        {
            Console.SetCursorPosition(2, 3);
            Console.Write("Would you like to continue? (y/n) ");

            string input = Console.ReadLine();
            if (input == "y")
            {
                Console.Clear();
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
            Write("╔═════════╗", row, col);
            Write("║ Well    ║", row + 1, col);
            Write("║   done! ║", row + 2, col);
            Write("╚═════════╝", row + 3, col);
        }

        private void PrintGameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int row = field.TopY / 2 - 1;
            int col = field.LeftX / 2 - 6;
            Write("╔═════════╗", row, col);
            Write("║ Game    ║", row + 1, col);
            Write("║   over! ║", row + 2, col);
            Write("╚═════════╝", row + 3, col);
        }

        private void Write(string text, int row, int col)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(text);
        }
    }
}

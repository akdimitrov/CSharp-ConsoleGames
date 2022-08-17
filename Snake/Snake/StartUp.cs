using System;
using System.Text;
using SnakeGame.Core;
using SnakeGame.GameObjects;

namespace SnakeGame
{
    public class StartUp
    {
        private const int FieldCols = 60;
        private const int FieldRows = 20;

        public static void Main()
        {
            Console.Title = "Snake v1.0";
            Console.OutputEncoding = Encoding.Unicode;
            Console.SetWindowSize(FieldCols, FieldRows + 4);
            Console.SetBufferSize(FieldCols, FieldRows + 4);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;

            Wall wall = new Wall(FieldCols, FieldRows);
            Snake snake = new Snake(wall);
            IEngine engine = new Engine(wall, snake);
            engine.Run();
        }
    }
}

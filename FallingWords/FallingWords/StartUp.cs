using System;
using FallingWords.Core;
using FallingWords.Enums;
using FallingWords.GameObjects;

namespace FallingWords
{
    public class StartUp
    {
        public static void Main()
        {
            int fieldCols = 60;
            int fieldRows = 20;

            Console.Title = "Falling Words v1.0";
            Console.CursorVisible = false;
            Console.SetWindowSize(fieldCols, fieldRows + 2);
            Console.SetBufferSize(fieldCols, fieldRows + 2);

            Menu menu = new Menu(fieldCols, fieldRows);
            Level level = (Level)menu.Run();

            Field field = new Field(fieldCols, fieldRows);
            WordPool wordPool = new WordPool(field, level);

            Engine engine = new Engine(field, wordPool);
            engine.Run();
        }
    }
}

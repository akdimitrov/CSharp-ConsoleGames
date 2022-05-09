using System;

namespace FallingWords
{
    public class StartUp
    {
        public static void Main()
        {
            Console.Title = "Falling Words v1.0";
            Console.CursorVisible = false;
            int fieldRows = 20;
            int fieldCols = 60;
            Level level = Level.Medium;  // Easy, Medium, Hard

            Field field = new Field(fieldCols, fieldRows);
            Engine engine = new Engine(field, level);
            engine.Run();
        }
    }
}

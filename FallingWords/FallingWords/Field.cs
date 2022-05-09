using System;

namespace FallingWords
{
    public class Field : Point
    {
        private const char WallSymboll = '\u25A0';

        public Field(int leftX, int topY) : base(leftX, topY)
        {
            Console.WindowHeight = TopY + 1;
            Console.WindowWidth = LeftX;
            Console.BufferHeight = TopY + 1;
            Console.BufferWidth = LeftX;

            InitializeField();
        }

        private void DrawHorizontalLine(int topY)
        {
            for (int i = 0; i < LeftX; i++)
            {
                Draw(i, topY, WallSymboll);
            }
        }

        private void DrawVerticalLine(int leftX)
        {
            for (int i = 0; i < TopY; i++)
            {
                Draw(leftX, i, WallSymboll);
            }
        }

        public void InitializeField()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            DrawHorizontalLine(0);
            DrawHorizontalLine(2);
            DrawHorizontalLine(TopY);
            DrawVerticalLine(0);
            DrawVerticalLine(LeftX - 1);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

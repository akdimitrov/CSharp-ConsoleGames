﻿using System;

namespace FallingWords
{
    public abstract class Point
    {
        public Point(int leftX, int topY)
        {
            LeftX = leftX;
            TopY = topY;
        }

        public int LeftX { get; set; }

        public int TopY { get; set; }

        public void Draw(int leftX, int topY, char symbol)
        {
            Console.SetCursorPosition(leftX, topY);
            Console.Write(symbol);
        }
    }
}

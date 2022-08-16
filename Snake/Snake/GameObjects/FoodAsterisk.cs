using System;

namespace SnakeGame.GameObjects
{
    public class FoodAsterisk : Food
    {
        private const char FoodSymbol = '*';
        private const int AsteriskPoints = 1;
        private const ConsoleColor FoodColor = ConsoleColor.Cyan;

        public FoodAsterisk(Wall wall) : base(wall, FoodSymbol, AsteriskPoints, FoodColor)
        {
        }
    }
}

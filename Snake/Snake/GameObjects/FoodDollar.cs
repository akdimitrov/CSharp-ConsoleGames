using System;

namespace SnakeGame.GameObjects
{
    public class FoodDollar : Food
    {
        private const char FoodSymbol = '$';
        private const int DollarPoints = 2;
        private const ConsoleColor FoodColor = ConsoleColor.Green;

        public FoodDollar(Wall wall) : base(wall, FoodSymbol, DollarPoints, FoodColor)
        {
        }
    }
}

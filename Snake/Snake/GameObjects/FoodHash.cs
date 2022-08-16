using System;

namespace SnakeGame.GameObjects
{
    public class FoodHash : Food
    {
        private const char FoodSymbol = '#';
        private const int HashPoints = 3;
        private const ConsoleColor FoodColor = ConsoleColor.DarkYellow;

        public FoodHash(Wall wall) : base(wall, FoodSymbol, HashPoints, FoodColor)
        {
        }
    }
}

using System;

namespace SnakeGame.GameObjects
{
    public class FoodApple : Food
    {
        private const char FoodSymbol = '@';
        private const int ApplePoints = 4;
        private const ConsoleColor FoodColor = ConsoleColor.Red;

        public FoodApple(Wall wall) : base(wall, FoodSymbol, ApplePoints, FoodColor)
        {
        }
    }
}

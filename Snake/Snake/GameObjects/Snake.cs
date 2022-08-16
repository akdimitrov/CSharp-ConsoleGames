using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SnakeGame.GameObjects
{
    public class Snake
    {
        private const char SnakeSymbol = '\u25CF';
        private readonly Queue<Point> snakeElements;
        private readonly List<Food> food;
        private readonly Wall wall;
        private int foodIndex;
        private int nextLeftX;
        private int nextTopY;
        private int score;

        public Snake(Wall wall)
        {
            this.wall = wall;
            this.snakeElements = new Queue<Point>();
            this.food = new List<Food>();
            this.GetFoods();
            this.CreateSnake();
        }

        public int Score => score;

        protected int RandomFoodNumber => new Random().Next(0, this.food.Count);

        public bool IsMoving(Point direction)
        {
            Point currentSnakeHead = this.snakeElements.Last();
            GetNextPoint(direction, currentSnakeHead);

            if (IsPointOfSnake())
            {
                return false;
            }

            Point snakeNewHead = new Point(this.nextLeftX, this.nextTopY);

            if (IsPointOfWall(snakeNewHead))
            {
                return false;
            }

            this.snakeElements.Enqueue(snakeNewHead);
            snakeNewHead.Draw(SnakeSymbol);

            if (food[foodIndex].IsFoodPoint(snakeNewHead))
            {
                this.score += food[foodIndex].FoodPoints;
                this.Eat(direction, currentSnakeHead);
            }

            Point snakeTail = this.snakeElements.Dequeue();
            snakeTail.Draw(' ');

            return true;
        }

        private void Eat(Point direction, Point currentSnakeHead)
        {
            int length = food[foodIndex].FoodPoints;

            for (int i = 0; i < length; i++)
            {
                this.snakeElements.Enqueue(new Point(this.nextLeftX, this.nextTopY));
                GetNextPoint(direction, currentSnakeHead);
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void CreateSnake()
        {
            for (int topY = 1; topY <= 6; topY++)
            {
                this.snakeElements.Enqueue(new Point(2, topY));
            }

            this.foodIndex = this.RandomFoodNumber;
            this.food[foodIndex].SetRandomPosition(this.snakeElements);
        }

        private void GetFoods()
        {
            Type[] foodTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.Name.StartsWith("Food") && !t.IsAbstract)
                .ToArray();

            foreach (var type in foodTypes)
            {
                Food currentFood = (Food)Activator.CreateInstance(type, new object[] { this.wall });
                this.food.Add(currentFood);
            }
        }

        private void GetNextPoint(Point direction, Point snakeHead)
        {
            this.nextLeftX = snakeHead.LeftX + direction.LeftX;
            this.nextTopY = snakeHead.TopY + direction.TopY;
        }

        private bool IsPointOfWall(Point snake)
        {
            return snake.TopY == 0 || snake.LeftX == 0 || snake.LeftX == this.wall.LeftX - 1 || snake.TopY == this.wall.TopY;
        }

        private bool IsPointOfSnake()
        {
            return this.snakeElements.Any(x => x.LeftX == nextLeftX && x.TopY == nextTopY);
        }
    }
}

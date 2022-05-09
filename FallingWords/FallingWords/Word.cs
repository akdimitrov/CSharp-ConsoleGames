using System;
using System.Collections.Generic;

namespace FallingWords
{
    public class Word : Point
    {
        private readonly Queue<char> letters;

        public Word(string word, Field field) : base(0, 3)
        {
            letters = new Queue<char>(word);
            Points = word.Length;
            LeftX = new Random().Next(5, field.LeftX - word.Length - 5);
            Color = ConsoleColor.DarkGray;
        }

        public char FirstLetter => letters.Peek();

        public ConsoleColor Color { get; set; }

        public int Points { get; }

        public bool IsEmpty => letters.Count == 0;

        public void Print()
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(LeftX, TopY);
            Console.Write(string.Join("", letters));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Clear()
        {
            Console.SetCursorPosition(LeftX, TopY);
            Console.Write(new string(' ', letters.Count));
        }

        public void RemoveFirstLetter()
        {
            Clear();
            letters.Dequeue();
            Print();
        }
    }
}

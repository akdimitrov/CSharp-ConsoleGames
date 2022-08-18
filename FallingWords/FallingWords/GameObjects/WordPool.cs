using System;
using System.Collections.Generic;
using System.Linq;
using FallingWords.Enums;
using FallingWords.Utilities;

namespace FallingWords.GameObjects
{
    public class WordPool
    {
        private readonly WordGenerator wordGenerator;
        private readonly Queue<Word> wordPool;
        private readonly Random random;
        private readonly Field field;
        private readonly Level level;
        private int correctWords;
        private int score;

        public WordPool(Field field, Level level)
        {
            this.field = field;
            this.level = level;
            wordPool = new Queue<Word>();
            wordGenerator = new WordGenerator(field);
            random = new Random();
        }

        public Word FirstWord => wordPool.Any() ? wordPool.Peek() : null;

        public int Score => score;

        public int CorrectWords => correctWords;

        public bool IsMoving()
        {
            if (FirstWord != null && FirstWord.TopY + 1 == field.TopY)
            {
                FirstWord.Color = ConsoleColor.Red;
                FirstWord.Print();
                return false;
            }

            MoveWords();

            if (!wordPool.Any() || random.Next(3) <= (int)level)
            {
                GenerateRandomWord();
            }

            return true;
        }

        public void ProccessInput(char letter)
        {
            if (!wordPool.Any())
            {
                return;
            }

            if (letter != FirstWord.FirstLetter)
            {
                FirstWord.Color = ConsoleColor.Red;
                FirstWord.Print();
                return;
            }

            FirstWord.Color = ConsoleColor.White;
            FirstWord.RemoveFirstLetter();

            if (FirstWord.IsEmpty)
            {
                score += FirstWord.Points;
                correctWords++;
                wordPool.Dequeue();

                if (FirstWord != null)
                {
                    FirstWord.Color = ConsoleColor.White;
                    FirstWord.Print();
                }
            }
        }

        private void MoveWords()
        {
            foreach (var word in wordPool)
            {
                if (word == FirstWord && FirstWord.Color != ConsoleColor.Red)
                {
                    word.Color = ConsoleColor.White;
                }

                word.Clear();
                word.TopY++;
                word.Print();
            }
        }

        private void GenerateRandomWord()
        {
            Word newWord = wordGenerator.RandomWord;
            wordPool.Enqueue(newWord);
            if (newWord == FirstWord)
            {
                newWord.Color = ConsoleColor.White;
            }

            newWord.Print();
        }
    }
}

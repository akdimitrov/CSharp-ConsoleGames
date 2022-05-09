using System;
using System.IO;

namespace FallingWords
{
    class WordGenerator
    {
        private readonly string[] words;
        private readonly Field field;
        private readonly Random random;

        public WordGenerator(Field field)
        {
            using (StreamReader reader = new StreamReader("../../../words.txt"))
            {
                words = reader.ReadToEnd().Split(Environment.NewLine);
            }

            this.field = field;
            random = new Random();
        }

        public Word RandomWord => new Word(words[random.Next(0, words.Length)], field);
    }
}

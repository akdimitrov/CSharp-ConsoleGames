using System;
using FallingWords.Enums;

namespace FallingWords.GameObjects
{
    public class Menu : Point
    {
        private const string GameNameLogo = @"
  ______    _ _ _          __          __           _     
 |  ____|  | | (_)         \ \        / /          | |    
 | |__ __ _| | |_ _ __   __ \ \  /\  / /__  _ __ __| |___ 
 |  __/ _` | | | | '_ \ / _` \ \/  \/ / _ \| '__/ _` / __|
 | | | (_| | | | | | | | (_| |\  /\  / (_) | | | (_| \__ \
 |_|  \__,_|_|_|_|_| |_|\__, | \/  \/ \___/|_|  \__,_|___/
                         __/ |                            
                        |___/                             ";
        private const string Footer = "more at https://github.com/akdimitrov";
        private const string Prompt = "Select Difficulty Level";

        private int leftX;
        private int topY;
        private string[] levelOptions;
        private int selectedIndex;

        public Menu(int leftX, int topY) : base(leftX, topY)
        {
            this.levelOptions = new string[] { nameof(Level.Easy), nameof(Level.Normal), nameof(Level.Hard) };
            this.selectedIndex = 0;
            this.leftX = leftX / 2;
            this.topY = topY / 2 + 3;
        }

        public int Run()
        {
            ConsoleKey pressdKey;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Draw(this.leftX - Prompt.Length / 2, 0, GameNameLogo);
                Draw(LeftX - Footer.Length - 1, TopY, Footer);
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                pressdKey = keyInfo.Key;

                if (pressdKey == ConsoleKey.DownArrow)
                {
                    selectedIndex++;
                    if (selectedIndex == levelOptions.Length)
                    {
                        selectedIndex = 0;
                    }
                }
                else if (pressdKey == ConsoleKey.UpArrow)
                {
                    selectedIndex--;
                    if (selectedIndex == -1)
                    {
                        selectedIndex = levelOptions.Length - 1;
                    }
                }

            } while (pressdKey != ConsoleKey.Enter);

            Console.Clear();
            return selectedIndex;
        }

        private void DisplayOptions()
        {
            Draw(this.leftX - Prompt.Length / 2, this.topY - levelOptions.Length, Prompt);

            for (int i = 0; i < levelOptions.Length; i++)
            {
                string currentOption = levelOptions[i];
                string prefix;
                string suffix;

                if (i == selectedIndex)
                {
                    prefix = ">>";
                    suffix = "<<";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = "  ";
                    suffix = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                currentOption = $"{prefix} {currentOption} {suffix}";
                Draw(this.leftX - currentOption.Length / 2, this.topY + i - 1, currentOption);
            }

            Console.ResetColor();
        }
    }
}

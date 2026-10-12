using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.HelperFunctions
{
    internal class InputHandler
    {

        public static string CheckLettersOnly()
        {
            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No input entered. Try again. \n");
                    continue;
                }

                if (!input.All(c => c >= 'a' && c <= 'z'))
                {
                    Console.WriteLine("Only letters a-z are allowed. \n");
                    continue;
                }

                return input;
            }
        }

        public static string StartGameOptions()
        {

            while (true)
            {
                string? input = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("No input entered. Try again. \n");
                    continue;
                }

                if (!input.All(char.IsDigit) || !int.TryParse(input, out int players))
                {
                    Console.WriteLine("Please enter a number between 1 and 6.");
                    continue;
                }

                if (players < 1 || players > 6)
                {
                    Console.WriteLine("Number of players must be between 1 and 6.");
                    continue;
                }
                return input;

            }





        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.HelperFunctions
{
    public class ConsolePrinter
    {
        public static void PrintBustedMessage(string playerName, int handValue)
        {
            var originalBg = Console.BackgroundColor;
            var originalFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{playerName} busted with a hand value of {handValue}. \n");
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;
        }

        public static void PrintPlayerStandMessage(string playerName, int handValue)
        {
            var originalBg = Console.BackgroundColor;
            var originalFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{playerName} stands with a hand value of {handValue}. \n");
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;
        }

        public static void PrintDealerWinsMessage()
        {
            var originalBg = Console.BackgroundColor;
            var originalFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Dealer wins! \n");
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;
        }

        public static void PrintPlayerWinsMessage(string playerName)
        {
            var originalBg = Console.BackgroundColor;
            var originalFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{playerName} wins! \n");
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;
        }

        public static void PrintPlayerTieMessage(string playerName)
        {
            var originalBg = Console.BackgroundColor;
            var originalFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"{playerName} ties! \n");
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;
        }

        public static void PrintNoChipsMessage(string playerName)
        {
            var originalBg = Console.BackgroundColor;
            var originalFg = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{playerName} has no chips left and cannot continue playing. \n");
            Console.BackgroundColor = originalBg;
            Console.ForegroundColor = originalFg;
        }

    }
}

using BlackJack.Entities;
using BlackJack.HelperFunctions;

namespace BlackJack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the amount of players for this game:");
            int input = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"You entered: {input}");

            List<Player> players = new List<Player>();
            for (int i = 0; i < input; i++)
            {
                Console.WriteLine($"Please enter the name of player {i + 1}:");
                string playerName = Console.ReadLine();
                Player player = new Player(playerName);
                players.Add(player);
                Console.WriteLine();
            }

            GameHandler gameHandler = new GameHandler(players);
            while (true)
            {
                gameHandler.StartGame();
                if(gameHandler.players.Count == 0)
                {
                    Console.WriteLine("All players are out of chips. Game over.");
                    break;
                }

            }

        }

    }
}

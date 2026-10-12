using BlackJack.Entities;
using BlackJack.HelperFunctions;

namespace BlackJack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter an amount of players (1-6) for this game: \n");
            string input = InputHandler.StartGameOptions();
            Console.WriteLine($"Starting a game with {input} players: \n");


            List<Player> players = new List<Player>();
            for (int i = 0; i < Convert.ToInt32(input); i++)
            {
                Console.WriteLine($"Please enter the name of player {i + 1}: \n");
                string playerName = InputHandler.CheckLettersOnly();
                Player player = new Player(playerName);
                players.Add(player);
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

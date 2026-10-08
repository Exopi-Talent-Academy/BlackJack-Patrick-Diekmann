using BlackJack.Entities;
using BlackJack.HelperFunctions;
using System.Security.AccessControl;

namespace BlackJack
{
    internal class GameHandler
    {
        public List<Player> players;
        public Shoe shoe;
        public Dealer dealer;
        public GameState gameState;

        public GameHandler(List<Player> players)
        {
            this.players = players;
            this.shoe = new Shoe();
            this.dealer = new Dealer("Dealer");
            this.gameState = new GameState
            {
                players = this.players,
                dealer = this.dealer,
                shoe = this.shoe,
                discardPile = new DiscardPile()
            };
        }

        public bool StartGame()
        {
            List<Player> playerResults = new List<Player>();

            // Deal initial hands to players and the dealer
            for (int i = 0; i < this.players.Count; i++)
            {
                this.players[i].ResetHand(this.gameState.discardPile);
                List<Card> initialHand = this.shoe.GetCards(2, this.gameState.discardPile);
                this.players[i].NewHand(initialHand);
                this.players[i].handValue = this.players[i].GetHandValue();
            }
            this.dealer.NewHand(this.shoe.GetCards(2, this.gameState.discardPile));

            // Each player's turn: loop until they stand or bust
            for (int i = 0; i < this.players.Count; i++)
            {
                Player player = this.players[i];
                if(player.chips <= 0)
                {
                    ConsolePrinter.PrintNoChipsMessage(player.name);
                    this.players.Remove(player);
                    i--;
                    continue;
                }
                int playerHandValue = player.TakeTurn(shoe, player, this.gameState.discardPile);
                if (!(playerHandValue > 21))
                {
                    playerResults.Add(player);
                }

            }

            // Dealer's turn: loop until they reach 17 or higher
            int dealerHandValue = dealer.TakeTurn(shoe, playerResults, this.gameState.discardPile);
            if (dealerHandValue > 21)
            {
                ConsolePrinter.PrintBustedMessage(dealer.name, dealerHandValue);
            }
            else
            {
                ConsolePrinter.PrintPlayerStandMessage(dealer.name, dealerHandValue);
            }

            GameResult gameResult = new GameResult(playerResults, dealer);
            (bool dealerWon, List<Player> winningPlayers, List<Player> tiePlayers) = gameResult.DetermineGameResult();

            if (dealerWon)
            {
                ConsolePrinter.PrintDealerWinsMessage();
            }
            else
            {
                if (winningPlayers.Count > 0)
                {
                    Console.WriteLine("Winning players: \n");
                    foreach (Player player in winningPlayers)
                    {
                        player.chips += 2;
                        ConsolePrinter.PrintPlayerWinsMessage(player.name);
                    }
                }
                if (tiePlayers.Count > 0)
                {
                    Console.WriteLine("Tie players: \n");
                    foreach (Player player in tiePlayers)
                    {
                        player.chips += 1;
                        ConsolePrinter.PrintPlayerTieMessage(player.name);
                    }
                }

            }
            return true;
        }

        public GameState EndGame()
        {
            this.gameState.players = this.players;
            this.gameState.dealer = this.dealer;
            this.gameState.shoe = this.shoe;
            return this.gameState;
        }


    }
}

using BlackJack.Entities;
using BlackJack.HelperFunctions;

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
            EnsureShoeHasEnoughCards();
            // Deal initial hands to players and the dealer
            for (int i = 0; i < players.Count; i++)
            {
                List<Card> initialHand = shoe.GetCards(2);
                players[i].NewHand(initialHand);
                players[i].handValue = players[i].GetHandValue();
            }
            dealer.NewHand(shoe.GetCards(2));

            // Each player's turn: loop until they stand or bust
            for (int i = 0; i < players.Count; i++)
            {
                Player player = players[i];
                if (player.chips <= 0)
                {
                    ConsolePrinter.PrintNoChipsMessage(player.name);
                    players.Remove(player);
                    i--;
                    continue;
                }
                int playerHandValue = player.TakeTurn(shoe, player);
                if (!(playerHandValue > 21))
                {
                    playerResults.Add(player);
                }

            }

            // Dealer's turn: loop until they reach 17 or higher
            int dealerHandValue = dealer.TakeTurn(shoe, playerResults);
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

            // Hand is over and we add all players' hands and the dealer's hand to the discard pile
            foreach (Player player in players)
            {
                gameState.discardPile.AddCards(player.hand);
                player.ResetHand();
            }
            gameState.discardPile.AddCards(dealer.hand);
            dealer.ResetHand();

            return true;
        }

        public void EnsureShoeHasEnoughCards()
        {
            // player count + dealer count * 4. It aint perfect but its a fix for now
            if (shoe.cards.Count < (players.Count + 1) * 4)
            {
                shoe.ResetShoe(gameState.discardPile);
                gameState.discardPile.cards.Clear();
            }
        }

        public GameState EndGame()
        {
            gameState.players = players;
            gameState.dealer = dealer;
            gameState.shoe = shoe;
            return gameState;
        }


    }
}

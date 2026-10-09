using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class GameResult
    {
        public bool dealerWon { get; set; }
        public List<Player> players { get; set; }
        public List<Player> winningPlayers { get; set; }
        public List<Player> tiePlayers { get; set; }
        public Dealer dealer { get; set; }

        public GameResult(List<Player> players, Dealer dealer)
        {
            this.players = players;
            this.dealer = dealer;
            this.dealerWon = false;
        }

        // returns a tuple containing a boolean indicating if the dealer won, a list of winning players, and a list of tie players
        public (bool, List<Player>, List<Player>) DetermineGameResult()
        {
            winningPlayers = new List<Player>();
            tiePlayers = new List<Player>();
            int dealerHandValue = dealer.GetHandValue();
            foreach (Player player in players)
            {
                int playerHandValue = player.GetHandValue();

                // Player busts, dealer wins
                if (playerHandValue > 21)
                {
                    continue;
                }
                // Dealer busts or player has a higher hand value, player wins
                else if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
                {
                    winningPlayers.Add(player);
                }
                // Player has blackjack and dealer does not, player wins
                else if (player.HasBlackJack() && !dealer.HasBlackJack())
                {
                    winningPlayers.Add(player);
                }
                // Dealer has blackjack and player does not, dealer wins
                else if (!player.HasBlackJack() && dealer.HasBlackJack())
                {
                    continue;
                }
                else if (playerHandValue == dealerHandValue)
                {
                    // Tie, no one wins
                    tiePlayers.Add(player);
                }


            }

            // If no players won or tied, dealer wins
            if (winningPlayers.Count == 0 && tiePlayers.Count == 0)
            {
                dealerWon = true;
            }
            return (dealerWon, winningPlayers, tiePlayers);
        }
    }
}

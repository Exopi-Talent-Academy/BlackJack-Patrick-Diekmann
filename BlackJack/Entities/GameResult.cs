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
            this.winningPlayers = new List<Player>();
            this.tiePlayers = new List<Player>();
            int dealerHandValue = this.dealer.GetHandValue();
            foreach (Player player in this.players)
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
                    this.winningPlayers.Add(player);
                }
                // Player has blackjack and dealer does not, player wins
                else if (player.HasBlackJack() && !this.dealer.HasBlackJack())
                {
                    this.winningPlayers.Add(player);
                }
                // Dealer has blackjack and player does not, dealer wins
                else if (!player.HasBlackJack() && this.dealer.HasBlackJack())
                {
                    continue;
                }
                else if (playerHandValue == dealerHandValue)
                {
                    // Tie, no one wins
                    this.tiePlayers.Add(player);
                }


            }

            // If no players won or tied, dealer wins
            if (this.winningPlayers.Count == 0 && this.tiePlayers.Count == 0)
            {
                this.dealerWon = true;
            }
            return (this.dealerWon, this.winningPlayers, this.tiePlayers);
        }
    }
}

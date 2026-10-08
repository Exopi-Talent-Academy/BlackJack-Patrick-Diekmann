using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class CurrentGameState
    {
        public bool dealerWon { get; set; }
        public List<Player> winningPlayers { get; set; }

        public (bool, List<Player>) DetermineWinners(Dealer dealer, List<Player> players)
        {
            this.winningPlayers = new List<Player>();
            this.dealerWon = false;
            int dealerHandValue = dealer.GetHandValue();
            foreach (Player player in players)
            {
                int playerHandValue = player.GetHandValue();
                if (playerHandValue > 21)
                {
                    // Player busts, dealer wins
                    continue;
                }
                else if (dealerHandValue > 21 || playerHandValue > dealerHandValue)
                {
                    // Dealer busts or player has a higher hand value, player wins
                    this.winningPlayers.Add(player);
                }
                else if (playerHandValue == dealerHandValue)
                {
                    // Tie, no one wins
                    continue;
                }
                else
                {
                    // Dealer has a higher hand value, dealer wins
                    this.dealerWon = true;
                }
            }
            return (this.dealerWon, this.winningPlayers);
        }
    }
}

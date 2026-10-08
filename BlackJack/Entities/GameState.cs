using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class GameState
    {
        public List<Player> players { get; set; }
        public Dealer dealer { get; set; }
        public Shoe shoe { get; set; }
        public DiscardPile discardPile { get; set; }

    }
}

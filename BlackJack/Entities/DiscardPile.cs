using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class DiscardPile
    {   
        public List<Card> cards { get; set; }

        public DiscardPile()
        {
            this.cards = new List<Card>();
        }

        public void AddCards(List<Card> cards)
        {
            cards.AddRange(cards);
        }

    }
}

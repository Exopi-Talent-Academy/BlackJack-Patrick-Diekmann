using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class Dealer
    {
        public string name { get; private set; }
        public List<Card> hand { get; private set; }
        public int handValue { get; set; }

        public Dealer(string name = "Dealer")
        {
            this.name = name;
            this.hand = new List<Card>();
        }

        public void NewHand(List<Card> cards)
        {
            this.hand = new List<Card>(cards);
            this.handValue = this.GetHandValue();
        }

        public void Hit(Card card)
        {
            this.hand.Add(card);
        }

        public int GetHandValue()
        {
            int value = 0;
            int aceCount = 0;
            foreach (Card card in this.hand)
            {
                value += card.GetValue();
                if (card.rank == Card.Rank.Ace)
                {
                    aceCount++;
                }
            }

            // Save for later maybe
            //while (value > 21 && aceCount > 0)
            //{
            //    value -= 10;
            //    aceCount--;
            //}

            return value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Dealer: {this.name}");
            sb.AppendLine("Hand:");
            foreach (Card card in this.hand)
            {
                sb.AppendLine(card.ToString());
            }
            sb.AppendLine($"Total Value: {this.GetHandValue()}");
            return sb.ToString();
        }

        public int TakeTurn(Shoe shoe, List<Player> activePlayers, DiscardPile discardPile)
        {
            if(activePlayers.Count == 0)
            {
                Console.WriteLine("No active players left. Dealer does not take a turn as he already won \n");
                return this.GetHandValue();
            }

            while (this.GetHandValue() <= 17)
            {
                Card newCard = shoe.GetCards(1, discardPile)[0];
                this.Hit(newCard);
                Console.WriteLine($"Dealer drew: {newCard}");
                Console.WriteLine($"Dealer's hand value is now: {this.GetHandValue()} \n");
            }
            return this.GetHandValue();
        }

        public bool HasBlackJack()
        {
            return this.hand.Count == 2 && this.GetHandValue() == 21;
        }
    }
}

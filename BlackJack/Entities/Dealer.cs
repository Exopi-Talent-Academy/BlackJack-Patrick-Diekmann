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
           hand = new List<Card>(cards);
            handValue = GetHandValue();
        }

        public void Hit(Card card)
        {
            hand.Add(card);
        }

        public void ResetHand()
        {
            handValue = 0;
            hand.Clear();
        }

        public int GetHandValue()
        {
            int value = 0;
            int aceCount = 0;
            foreach (Card card in hand)
            {
                value += card.GetValue();
                if (card.rank == Card.Rank.Ace)
                {
                    aceCount++;
                }
            }

            // Save for later maybe
            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }

            return value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Dealer: {name}");
            sb.AppendLine("Hand:");
            foreach (Card card in hand)
            {
                sb.AppendLine(card.ToString());
            }
            sb.AppendLine($"Total Value: {GetHandValue()}");
            return sb.ToString();
        }

        public int TakeTurn(Shoe shoe, List<Player> activePlayers)
        {
            if(activePlayers.Count == 0)
            {
                Console.WriteLine("No active players left. Dealer does not take a turn as he already won \n");
                return GetHandValue();
            }

            while (GetHandValue() <= 17)
            {
                Card newCard = shoe.GetCards(1)[0];
                Hit(newCard);
                Console.WriteLine($"Dealer drew: {newCard}");
                Console.WriteLine($"Dealer's hand value is now: {GetHandValue()} \n");
            }
            return GetHandValue();
        }

        public bool HasBlackJack()
        {
            return hand.Count == 2 && GetHandValue() == 21;
        }
    }
}

using BlackJack.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class Deck
    {
        public List<Card> cards { get; private set; }

        public Deck()
        {
            this.cards = new List<Card>();
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                {
                    this.cards.Add(new Card(suit, rank));
                }
            }
        }

        public Card GetCard()
        {
            if (this.cards.Count == 0)
            {
                // Depending on the implementation this may be done differently. We will randomly get a deck that is empty while the other decks are not
                // When that happens we will need to remove the empty deck from the shoe and try drawing a card again (this logic will happen in another class)
                throw new EmptyDeckException("No cards left in the deck.");
            }
            Random random = new Random();
            int index = random.Next(cards.Count);
            Card card = cards[index];
            cards.RemoveAt(index);
            return card;
        }

        public void printDeck()
        {
            foreach (Card card in cards)
            {
                Console.WriteLine(card.ToString());
            }
        }
    }
}

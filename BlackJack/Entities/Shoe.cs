using BlackJack.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack.Entities
{
    public class Shoe
    {
        public List<Deck> decks { get; private set; }


        public Shoe(int numberOfDecks = 1)
        {
            this.decks = new List<Deck>();
            for (int i = 0; i < numberOfDecks; i++)
            {
                this.decks.Add(new Deck());
            }
        }

        public List<Card> GetCards(int numberOfCards, DiscardPile discardPile)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < numberOfCards; i++)
            {
                try
                {
                    if (this.decks.Count == 0)
                    {
                        throw new EmptyShoeException("No more cards in the shoe.");
                    }
                    Deck deck = this.decks[0];
                    Card card = deck.GetCard();
                    cards.Add(card);
                }
                catch (EmptyDeckException)
                {
                    this.decks.RemoveAt(0);
                    Console.WriteLine("A deck has been popped");
                    i--; // Retry drawing a card from the next deck
                }
                catch (EmptyShoeException)
                {
                    // WE ARE STILL MISSING SOME REFERENCES WHERE WE NEED THE DISCARD PILE. FOLLOW THE RABBIT HOLE
                    i--; // Exit the loop if the shoe is empty
                }



            }
            return cards;
        }

        public void ResetShoe(int numberOfDecks = 1)
        {
            this.decks.Clear();
            for (int i = 0; i < numberOfDecks; i++)
            {
                this.decks.Add(new Deck());
            }
        }
}

namespace BlackJack.Entities
{
    public class Shoe
    {
        public List<Card> cards { get; private set; }

        public Shoe(int numberOfDecks = 1)
        {
            this.cards = new List<Card>();
            for (int i = 0; i < numberOfDecks; i++)
            {
                foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
                {
                    foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                    {
                        this.cards.Add(new Card(suit, rank));
                    }
                }
            }
        }

        public List<Card> GetCards(int numberOfCards = 1)
        {
            List<Card> cardsDealt = new List<Card>();
            try
            {
                // add first card in cards to cardsDealt and remove it from cards, repeat for numberOfCards times
                for (int i = 0; i < numberOfCards; i++)
                {
                    cardsDealt.Add(cards[0]);
                    cards.RemoveAt(0);
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Shoe is empty");
            }


            return cardsDealt;
        }

        public void ResetShoe(DiscardPile discardPile)
        {
            foreach(Card card in cards)
            {
                discardPile.cards.Add(card);
            }
            cards.Clear();
            foreach (Card card in discardPile.cards)
            {
                cards.Add(card);
            }
        }
    }
}

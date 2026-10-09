namespace BlackJack.Entities
{
    public class Card
    {
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }

        public enum Rank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public Suit suit { get; private set; }
        public Rank rank { get; private set; }

        public Card(Suit suit, Rank rank)
        {
            this.suit = suit;
            this.rank = rank;
        }

        public int GetValue()
        {
            switch (rank)
            {
                case Rank.Two:
                    return 2;

                case Rank.Three:
                    return 3;

                case Rank.Four:
                    return 4;

                case Rank.Five:
                    return 5;

                case Rank.Six:
                    return 6;

                case Rank.Seven:
                    return 7;

                case Rank.Eight:
                    return 8;

                case Rank.Nine:
                    return 9;

                case Rank.Ten:
                case Rank.Jack:
                case Rank.Queen:
                case Rank.King:
                    return 10;

                case Rank.Ace:
                    return 11;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            if (rank == Rank.Ace)
            {
                return $"Ace of {suit}";
            }
            else
            {
                return $"{GetValue()} of {suit}";
            }
        }
    }
}

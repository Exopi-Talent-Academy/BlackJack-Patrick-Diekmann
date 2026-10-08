using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using BlackJack.HelperFunctions;

namespace BlackJack.Entities
{
    public class Player
    {
        public string name { get; private set; }
        public List<Card> hand { get; private set; }
        public int handValue { get; set; }
        public int chips { get; set; } = 4;

        public Player(string name)
        {
            this.name = name;
            this.hand = new List<Card>();
            this.handValue = 0;
        }

        public void ResetHand(DiscardPile discardPile)
        {
            if(this.hand.Count > 0)
            {
                discardPile.AddCards(this.hand);
            }
            this.handValue = 0;
            this.hand.Clear();
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
            sb.AppendLine($"Player: {this.name}");
            sb.AppendLine($"Chips: {this.chips}");
            sb.AppendLine("Hand:");
            foreach (Card card in this.hand)
            {
                sb.AppendLine(card.ToString());
            }
            sb.AppendLine($"Total Value: {this.GetHandValue()}");
            return sb.ToString();
        }

        public int TakeTurn(Shoe shoe, Player player, DiscardPile discardPile)
        {
            this.chips -= 1;
            while (true)
            {
                Console.WriteLine($"Player {player.name}'s turn:");
                Console.WriteLine(player.ToString());
                Console.WriteLine("Do you want to hit or stand? (h/s)");

                char input = char.ToLower(Console.ReadKey(true).KeyChar);

                if (input == 'h') // Hit
                {
                    Console.Clear();
                    Console.WriteLine("Hit me! \n");
                    Card newCard = shoe.GetCards(1, discardPile)[0];
                    player.Hit(newCard);
                    Console.WriteLine($"You drew: {newCard}");
                    player.handValue += newCard.GetValue();
                    Console.WriteLine($"Your hand value is now: {player.handValue} \n");

                    if (player.handValue > 21)
                    {
                        ConsolePrinter.PrintBustedMessage(player.name, player.handValue);
                        break;
                    }
                }
                else if (input == 's') // Stand
                {
                    Console.Clear();
                    ConsolePrinter.PrintPlayerStandMessage(player.name, player.handValue);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'h' to hit or 's' to stand.");
                }
            }
            return player.handValue;
        }

        public bool HasBlackJack()
        {
            return this.hand.Count == 2 && this.GetHandValue() == 21;
        }
    }
}

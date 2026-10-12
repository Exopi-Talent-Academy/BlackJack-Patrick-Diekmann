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
        public int chips { get; set; } = 50;

        public Player(string name)
        {
            this.name = name;
            this.hand = new List<Card>();
            this.handValue = 0;
        }

        public void ResetHand()
        {
            handValue = 0;
            hand.Clear();
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

            //Save for later maybe
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
            sb.AppendLine($"Player: {name}");
            sb.AppendLine($"Chips: {chips}");
            sb.AppendLine("Hand:");
            foreach (Card card in hand)
            {
                sb.AppendLine(card.ToString());
            }
            sb.AppendLine($"Total Value: {GetHandValue()}");
            return sb.ToString();
        }

        public int TakeTurn(Shoe shoe, Player player)
        {
            chips -= 1;
            while (true)
            {
                Console.WriteLine($"Player {player.name}'s turn:");
                Console.WriteLine(player.ToString());
                Console.WriteLine("Do you want to hit or stand? (h/s) \n");

                char input = char.ToLower(Console.ReadKey(true).KeyChar);

                if (input == 'h') // Hit
                {
                    Console.Clear();
                    Console.WriteLine("Hit me! \n");
                    Card newCard = shoe.GetCards(1)[0];
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
            return hand.Count == 2 && GetHandValue() == 21;
        }
    }
}

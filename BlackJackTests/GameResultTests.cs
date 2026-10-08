using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BlackJack.Entities;

namespace BlackJackTests
{
    public class GameResultTests
    {

        [Fact]
        public void TestGetHandValue()
        {
            // Arrange
            var player = new Player("Test Player");
            var cards = new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Ace)
            };
            player.NewHand(cards);
            // Act
            int handValue = player.GetHandValue();
            // Assert
            Assert.Equal(21, handValue);
        }

        [Fact]
        public void DetermineGameResult_PlayerWins()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Seven)
            });

            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Eight)
            });

            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);

            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();

            // Assert
            Assert.False(dealerWon);
            Assert.Contains(player1, winningPlayers);
            Assert.Empty(tiePlayers);
        }

        [Fact]
        public void DetermineGameResult_DealerWins()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Seven)
            });

            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Nine),
                new Card(Card.Suit.Spades, Card.Rank.Six)
            });

            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);

            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();

            // Assert
            Assert.True(dealerWon);
            Assert.Empty(winningPlayers);
            Assert.Empty(tiePlayers);
        }

        [Fact]
        public void DetermineGameResult_Tie()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Seven)
            });

            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Nine),
                new Card(Card.Suit.Spades, Card.Rank.Eight)
            });

            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);

            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();

            // Assert
            Assert.False(dealerWon);
            Assert.Empty(winningPlayers);
            Assert.Contains(player1, tiePlayers);
        }

        [Fact]
        public void DetermineGameResult_AllPlayersBusts()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Seven)
            });

            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.King),
                new Card(Card.Suit.Spades, Card.Rank.Queen),
                new Card(Card.Suit.Diamonds, Card.Rank.Two)
            });

            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);

            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();

            // Assert
            Assert.True(dealer.handValue == 17);
            Assert.True(dealerWon);
            Assert.Empty(winningPlayers);
            Assert.Empty(tiePlayers);
        }

        [Fact]
        public void DetermineGameResult_DealerBusts()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.King),
                new Card(Card.Suit.Spades, Card.Rank.Queen),
                new Card(Card.Suit.Diamonds, Card.Rank.Two)
            });
            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Nine),
                new Card(Card.Suit.Spades, Card.Rank.Eight)
            });
            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);
            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();
            // Assert
            Assert.False(dealerWon);
            Assert.Contains(player1, winningPlayers);
            Assert.Empty(tiePlayers);
        }

        [Fact]
        public void DetermineGameResult_PlayerHasBlackJack()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Seven)
            });
            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ace),
                new Card(Card.Suit.Spades, Card.Rank.Jack)
            });
            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);
            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();
            // Assert
            Assert.False(dealerWon);
            Assert.Contains(player1, winningPlayers);
            Assert.Empty(tiePlayers);
        }

        [Fact]
        public void DetermineGameResult_DealerHasBlackJack()
        {
            // Arrange
            var dealer = new Dealer();
            dealer.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ace),
                new Card(Card.Suit.Spades, Card.Rank.Jack)
            });
            var player1 = new Player("Player 1");
            player1.NewHand(new List<Card>
            {
                new Card(Card.Suit.Hearts, Card.Rank.Ten),
                new Card(Card.Suit.Spades, Card.Rank.Seven)
            });
            var players = new List<Player> { player1 };
            var gameResult = new GameResult(players, dealer);
            // Act
            var (dealerWon, winningPlayers, tiePlayers) = gameResult.DetermineGameResult();
            // Assert
            Assert.True(dealerWon);
            Assert.Empty(winningPlayers);
            Assert.Empty(tiePlayers);
        }
    }
}

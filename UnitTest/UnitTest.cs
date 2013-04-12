namespace TexasHoldEmPoker.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TexasHoldEmPoker.Rank;

    [TestClass]
    public class UnitTest
    {
        private ICardHandPattern cardHandIdentifier;
        private IPokerEngine pokerEngine;

        [TestInitialize]
        public void SetupCardPattern()
        {
            var highCard = new HighCard();
            var onePair = new OnePair(highCard);
            var twoPair = new TwoPair(onePair);
            var threeOfAKind = new ThreeOfAKind(twoPair);
            var straight = new Straight(threeOfAKind);
            var flush = new Flush(straight);
            var fullHouse = new FullHouse(flush);
            var fourOfAKind = new FourOfAKind(fullHouse);
            var straightFlush = new StraightFlush(fourOfAKind, straight, flush);
            var royalFlush = new RoyalFlush(straightFlush, straight, flush);

            cardHandIdentifier = royalFlush;

            pokerEngine = new PokerEngine();
        }

        [TestMethod]
        public void TestAPIGenerateDeck()
        {
            var poker = new PokerEngine();
            var cardDeck = poker.GenerateDeck();

            var noOfCard = (Enum.GetValues(typeof(CardSuit)).Length - 1)
                           * (Enum.GetValues(typeof(CardValue)).Length - 1);
            Assert.AreEqual(noOfCard, cardDeck.Count());
            var wrongCardValue = cardDeck.Count(x => x.Value == CardValue.None);
            Assert.AreEqual(0, wrongCardValue);
            var wrongCardSuit = cardDeck.Count(x => x.Suit == CardSuit.None);
            Assert.AreEqual(0, wrongCardSuit);
        }

        [TestMethod]
        public void TestAPIDealRandomlyHandOfUniqueCards()
        {
            var cardHand = pokerEngine.DealRandomlyHandOfUniqueCards();

            Assert.AreEqual(5, cardHand.Count());
            var groupOfCards = (from card in cardHand
                          group card by new { card.Suit, card.Value } into cardGroup
                          select new { Count = cardGroup.Count() });
            Assert.AreEqual(5, groupOfCards.Count());
        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CardHandIsNotQualify()
        {
            try
            {
                var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Ten },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Jack },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Eight }
                };
                pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            }
            catch (ApplicationException exc)
            {
                Assert.AreEqual("Card hand contains less than 5 cards", exc.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void CardHandContainsDuplicatedCard()
        {
            try
            {
                var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Ten },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Jack },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Eight },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Eight }
                };
                pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            }
            catch (ApplicationException exc)
            {
                Assert.AreEqual("Card hand contains duplicated card", exc.Message);
                throw;
            }
        }

        [TestMethod]
        public void CardHandIsHighCard()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Six },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Ten },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Jack },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Eight }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.HighCard, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsOnePair()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Nine },
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Seven }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.OnePair, typeOfHand);

        }

        [TestMethod]
        public void CardHandIsTwoPair()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Three },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Three },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Five }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.TwoPair, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsThreeOfAKind()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Five },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Two }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.ThreeOfAKind, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsStraight()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Five },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Three },
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Two }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.Straight, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsFlush()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Three },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Seven },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Queen },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ten }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.Flush, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsFullHouse()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Diamon, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Clubs, Value = CardValue.Four }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.FullHouse, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsFourOfAKind()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Spades, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Three },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Queen },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Jack }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.FourOfAKind, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsStraightFlush()
        {
            var cardHand = new List<Card>
                {
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ace },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Two },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Three },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Four },
                    new Card() { Suit = CardSuit.Hearts, Value = CardValue.Five }
                };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.StraightFlush, typeOfHand);
        }

        [TestMethod]
        public void CardHandIsRoyalFlush()
        {
            var cardHand = new List<Card>
            {
                new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ten },
                new Card() { Suit = CardSuit.Hearts, Value = CardValue.Jack },
                new Card() { Suit = CardSuit.Hearts, Value = CardValue.Queen },
                new Card() { Suit = CardSuit.Hearts, Value = CardValue.King },
                new Card() { Suit = CardSuit.Hearts, Value = CardValue.Ace }
            };
            var typeOfHand = pokerEngine.IdentifyRankOfHand(cardHandIdentifier, cardHand);
            Assert.AreEqual(RankOfHand.RoyalFlush, typeOfHand);
        }
    }
}

namespace TexasHoldEmPoker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TexasHoldEmPoker.Rank;

    /// <summary>
    /// The public API
    /// </summary>
    public class PokerEngine: IPokerEngine
    {
        private int NoOfCardSuits = 4 ;
        private int NoOfCardValues = 13;
        private int NoOfCardInHand = 5;

        /// <summary>
        /// First requirement: generata a deck of 52 unique cards
        /// </summary>
        /// <returns></returns>
        public List<Card> GenerateDeck()
        {
            try
            {
                var cardDeck = new List<Card>();
                for (int i = 1; i <= NoOfCardSuits; i++)
                {
                    for (int j = 1; j <= NoOfCardValues; j++)
                    {
                        cardDeck.Add(new Card() { Suit = (CardSuit)i, Value = (CardValue)j });
                    }
                }

                return cardDeck;
            }
            catch(PokerException pex)
            {
                // code to write log file: datetime, description

                throw new ApplicationException(pex.Message,pex);
            }
        }

        /// <summary>
        /// Second requirement: Randomly deal a hand of 5 unique cards
        /// </summary>
        /// <returns></returns>
        public List<Card> DealRandomlyHandOfUniqueCards()
        {
            try
            {
                var cardHand = new List<Card>();
                var random = new Random();

                var cardEqualityComparer = new CardEqualityComparer();

                for (int i = 0; i < NoOfCardInHand; i++)
                {
                    var randomNumber = random.Next(1, NoOfCardSuits * NoOfCardValues);
                    while (cardHand.Contains(randomNumber.ToCard(NoOfCardValues), cardEqualityComparer))
                    {
                        randomNumber = random.Next(1, NoOfCardSuits * NoOfCardValues);
                    }
                    cardHand.Add(randomNumber.ToCard(NoOfCardValues));
                }

                return cardHand;
            }
            catch (PokerException pex)
            {
                // code to write log file: datetime, description

                throw new ApplicationException(pex.Message, pex);
            }

        }

        /// <summary>
        /// Third requirement: Identify type of hand
        /// </summary>
        /// <param name="cardEvaluator"> the evaluator is injected from client </param>
        /// <param name="cardHand"> the hand need to be evaluated</param>
        /// <returns>the rank of hand</returns>
        public RankOfHand IdentifyRankOfHand(ICardHandPattern cardEvaluator, List<Card> cardHand)
        {
            try
            {
                return cardEvaluator.ProccessRequest(cardHand);
            }
            catch (PokerException pex)
            {
                // code to write log file: datetime, description

                throw new ApplicationException(pex.Message, pex);
            }
        }
    }
}

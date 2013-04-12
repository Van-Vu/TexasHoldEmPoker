namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for other card pattern
    /// </summary>
    public class HighCard : ICardHandPattern
    {
        protected RankOfHand rank;

        protected ICardHandPattern Successor { get; set; }

        public HighCard()
        {
            rank = RankOfHand.HighCard;
        }

        /// <summary>
        /// Validate the hand before processing.
        /// </summary>
        /// <param name="cardHand"></param>
        public void GuardCheck(List<Card> cardHand)
        {
            if (cardHand.Count != 5)
            {
                throw new PokerException("Card hand contains less than 5 cards");
            }

            var result = (from card in cardHand
                          group card by new {card.Suit,card.Value} into cardGroup
                          select new { Count = cardGroup.Count() });
            if (result.Count() < 5)
            {
                throw new PokerException("Card hand contains duplicated card");
            }
        }

        public virtual bool IsQualifyRank(List<Card> cardHand)
        {
            return true;
        }

        public RankOfHand ProccessRequest(List<Card> cardHand)
        {
            GuardCheck(cardHand);

            // Each hand implements its own qualification rules
            if (IsQualifyRank(cardHand))
            {
                return rank;
            }

            if (this.Successor != null)
            {
                return this.Successor.ProccessRequest(cardHand);
            }

            // the hand is not qualify and there's no successor assigned.
            throw new PokerException(string.Format("Error happens at {0}", rank));
        }
    }
}

namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;

    /// <summary>
    /// Straight Flush - A straight of entirely one suit.
    /// </summary>
    public class StraightFlush:HighCard
    {
        private ICardHandPattern StraightQualifier;
        private ICardHandPattern FlushQualifier;

        public StraightFlush(ICardHandPattern successor, ICardHandPattern straight, ICardHandPattern flush)
        {
            Successor = successor;
            StraightQualifier = straight;
            FlushQualifier = flush;
            rank = RankOfHand.StraightFlush;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var isStraight = StraightQualifier.IsQualifyRank(cardHand);
            var isFlush = FlushQualifier.IsQualifyRank(cardHand);

            if (isStraight && isFlush)
            {
                return true;
            }

            return false;
        }
    }
}

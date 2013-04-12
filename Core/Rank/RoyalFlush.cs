namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Royal Flush - An Ace-High straight of one suit.
    /// </summary>
    public class RoyalFlush:HighCard
    {
        private ICardHandPattern StraightQualifier;
        private ICardHandPattern FlushQualifier;

        public RoyalFlush(ICardHandPattern successor, ICardHandPattern straight, ICardHandPattern flush)
        {
            Successor = successor;
            StraightQualifier = straight;
            FlushQualifier = flush;
            rank = RankOfHand.RoyalFlush;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var isStraight = StraightQualifier.IsQualifyRank(cardHand);
            var isFlush = FlushQualifier.IsQualifyRank(cardHand);

            List<Card> orderedList = cardHand.OrderByDescending(c => c.Value).ToList();

            if (isStraight && isFlush && orderedList[0].Value == (CardValue.King + 1) && orderedList[4].Value == CardValue.Ten)
            {
                return true;
            }

            return false;
        }
    }
}

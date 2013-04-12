namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;
    
    /// <summary>
    /// One Pair - Two cards of the same rank.
    /// </summary>
    public class OnePair : HighCard
    {
        public OnePair(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.OnePair;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var result = (from prod in cardHand
                          group prod by prod.Value into prodGroup
                          select new { Category = prodGroup.Key, ProductCount = prodGroup.Count() }).Count(x => x.ProductCount > 1);

            if (result == 1)
            {
                return true;
            }

            return false;

        }
    }
}

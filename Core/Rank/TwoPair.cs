namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Two Pair - Two cards of the same rank and another two cards of the same rank. 
    /// </summary>
    public class TwoPair : HighCard
    {
        public TwoPair(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.TwoPair;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var result = (from prod in cardHand
                          group prod by prod.Value into prodGroup
                          select new { Category = prodGroup.Key, ProductCount = prodGroup.Count() }).Count(x => x.ProductCount > 1);

            if (result == 2)
            {
                return true;
            }

            return false;
        }
    }
}

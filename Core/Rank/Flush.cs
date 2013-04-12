namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Flush - Five cards of the same suit.
    /// </summary>
    public class Flush: HighCard
    {
        public Flush(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.Flush;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var result = (from prod in cardHand
                          group prod by prod.Suit into prodGroup
                          select new { Category = prodGroup.Key, ProductCount = prodGroup.Count() }).Count(x => x.ProductCount == 5);

            if (result == 1)
            {
                return true;
            }

            return false;
        }
    }
}

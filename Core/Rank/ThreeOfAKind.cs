namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Three-of-a-Kind (Trips, Set, Triplets) - Three cards of the same rank.
    /// </summary>
    public class ThreeOfAKind: HighCard
    {
        public ThreeOfAKind(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.ThreeOfAKind;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var result = (from prod in cardHand
                          group prod by prod.Value into prodGroup
                          select new { Category = prodGroup.Key, ProductCount = prodGroup.Count() }).Count(x => x.ProductCount > 2);

            if (result == 1)
            {
                return true;
            }

            return false;
        }
    }
}

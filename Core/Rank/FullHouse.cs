namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Full House (Full Boat, Boat) - Three-of-a-kind and a pair
    /// </summary>
    public class FullHouse: HighCard
    {
        public FullHouse(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.FullHouse;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var result = from card in cardHand
                          group card by card.Value into cardGroup
                          select new { Count = cardGroup.Count() };
            if (result.Count() == 2 && result.Any(x => x.Count == 3) && result.Any(x => x.Count == 2))
            {
                return true;
            }

            return false;
        }
    }
}

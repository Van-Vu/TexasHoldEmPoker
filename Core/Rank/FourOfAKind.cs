namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Four-of-a-Kind (Quads) - Four cards of the same rank.
    /// </summary>
    public class FourOfAKind:HighCard
    {
        public FourOfAKind(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.FourOfAKind;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            var result = (from card in cardHand
                          group card by card.Suit into cardGroup
                          select new { Count = cardGroup.Count() }).Count(x => x.Count == 4);
            if (result == 1)
            {
                return true;
            }

            return false;
        }
    }
}

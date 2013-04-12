namespace TexasHoldEmPoker.Rank
{
    using System.Collections.Generic;

    public interface ICardHandPattern
    {
        bool IsQualifyRank(List<Card> cardHand);
        RankOfHand ProccessRequest(List<Card> cardHand);
    }
}

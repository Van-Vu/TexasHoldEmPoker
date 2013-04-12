namespace TexasHoldEmPoker
{
    using System.Collections.Generic;

    using TexasHoldEmPoker.Rank;

    public interface IPokerEngine
    {
        List<Card> GenerateDeck();

        List<Card> DealRandomlyHandOfUniqueCards();

        RankOfHand IdentifyRankOfHand(ICardHandPattern cardHandler, List<Card> CardHand);
    }
}

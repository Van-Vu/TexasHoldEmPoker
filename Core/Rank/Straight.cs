namespace TexasHoldEmPoker.Rank
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Straight (Run) - Five cards of sequential rank. Note that in holdem, Aces can be high or low.
    /// </summary>
    public class Straight: HighCard
    {
        public Straight(ICardHandPattern successor)
        {
            Successor = successor;
            rank = RankOfHand.Straight;
        }

        public override bool IsQualifyRank(List<Card> cardHand)
        {
            List<Card> orderedList = cardHand.OrderByDescending(c => c.Value).ToList();

            List<Card> uniqueList = orderedList.Where((c, i) => i == 0 || orderedList[i].Value != orderedList[i - 1].Value).ToList();

            // check if contains duplicate card
            if (uniqueList.Count < 5)
            {
                return false;
            }

            // in case of Ace high
            if (uniqueList[0].Value == CardValue.King && uniqueList[4].Value == CardValue.Ace)
            {
                uniqueList[4].Value += 13;
                uniqueList = uniqueList.OrderByDescending(c => c.Value).ToList();
            }

            // compare first and last card's value
            if (uniqueList[0].Value == uniqueList[4].Value + 4)
            {
                return true;
            }

            return false;
        }
    }
}

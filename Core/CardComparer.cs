namespace TexasHoldEmPoker
{
    using System.Collections.Generic;

    public class CardEqualityComparer : IEqualityComparer<Card>
    {
        public bool Equals(Card c1, Card c2)
        {
            if (c1.Suit == c2.Suit && c1.Value == c2.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetHashCode(Card c)
        {
            var hCode = (int)c.Value ^ (int)c.Suit;
            return hCode.GetHashCode();
        }
    }
}

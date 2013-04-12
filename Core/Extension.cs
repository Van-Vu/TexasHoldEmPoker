namespace TexasHoldEmPoker
{
    using System;

    public static class Extension
    {
        /// <summary>
        /// Convert int to Card instance.
        /// </summary>
        public static Card ToCard(this int number, int NoOfCardValues)
        {
            var suit = Math.Ceiling((double)number / NoOfCardValues);
            var value = number - ((suit - 1) * NoOfCardValues);
            return new Card() { Suit = (CardSuit)suit, Value = (CardValue)value };
        }
    }
}

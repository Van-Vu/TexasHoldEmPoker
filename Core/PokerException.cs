namespace TexasHoldEmPoker
{
    using System;

    public class PokerException : Exception
    {
        public PokerException(string message)
            : base(message)
        {
        }
    }
}

namespace TexasHoldEmPoker.Client
{
    using Autofac;
    using System;
    using TexasHoldEmPoker;
    using TexasHoldEmPoker.Rank;

    class Program
    {
        private static IContainer container;

        static void Main(string[] args)
        {
            try
            {
                SetupContainer();

                var poker = container.Resolve<IPokerEngine>();

                // 1st requirement
                var cardDeck = poker.GenerateDeck();

                // 2nd requirement
                var cardHand = poker.DealRandomlyHandOfUniqueCards();

                // 3rd requirement
                var retval = poker.IdentifyRankOfHand(container.Resolve<RoyalFlush>(), cardHand);
            }
            catch (Exception ex)
            {
                // write log file

                // print "user friendly" error message
                
            }

        }

        private static void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => new PokerEngine()).As<IPokerEngine>();
            builder.Register(c => new HighCard());
            builder.Register(c => new OnePair(c.Resolve<HighCard>()));
            builder.Register(c => new TwoPair(c.Resolve<OnePair>()));
            builder.Register(c => new ThreeOfAKind(c.Resolve<TwoPair>()));
            builder.Register(c => new Straight(c.Resolve<ThreeOfAKind>()));
            builder.Register(c => new Flush(c.Resolve<Straight>()));
            builder.Register(c => new FullHouse(c.Resolve<Flush>()));
            builder.Register(c => new FourOfAKind(c.Resolve<FullHouse>()));
            builder.Register(c => new StraightFlush(c.Resolve<FourOfAKind>(), c.Resolve<Straight>(), c.Resolve<Flush>()));
            builder.Register(c => new RoyalFlush(c.Resolve<StraightFlush>(), c.Resolve<Straight>(), c.Resolve<Flush>()));

            container = builder.Build();
        }
    }
}

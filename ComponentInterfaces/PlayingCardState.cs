namespace ComponentInterfaces
{
    public sealed class PlayingCardState
    {
        public PlayingCardState(CardSuits suit, CardRanks rank)
        {
            Suit = suit;
            Rank = rank;
        }

        public CardSuits Suit { get; }
        public CardRanks Rank { get; }
        
        private bool Equals(PlayingCardState other)
        {
            return Suit == other.Suit && Rank == other.Rank;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is PlayingCardState && Equals((PlayingCardState) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Suit*397) ^ (int) Rank;
            }
        }
    }
}

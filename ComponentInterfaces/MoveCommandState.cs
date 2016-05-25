namespace ComponentInterfaces
{
    public sealed class MoveCommandState
    {
        public CardsMainColumnState Previous { get; }
        public CardsMainColumnState Next { get; }
        public PlayingCardState Card { get; }

        public MoveCommandState(CardsMainColumnState previous, CardsMainColumnState next, PlayingCardState card)
        {
            Previous = previous;
            Next = next;
            Card = card;
        }
    }
}

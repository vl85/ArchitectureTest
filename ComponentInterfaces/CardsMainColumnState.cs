using System.Collections.Generic;

namespace ComponentInterfaces
{
    public sealed class CardsMainColumnState
    {
        public CardsMainColumnState(IReadOnlyCollection<PlayingCardState> cards)
        {
            Cards = cards;
        }

        public IReadOnlyCollection<PlayingCardState> Cards { get; }
    }
}

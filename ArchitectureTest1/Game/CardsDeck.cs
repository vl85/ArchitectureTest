using System;
using System.Collections.Generic;
using ComponentInterfaces;

namespace ArchitectureTest1.Game
{
    internal sealed class CardsDeck
    {
        internal List<PlayingCard> Cards { get; set; }

        internal static CardsDeck New()
        {
            var cards = new List<PlayingCard>();
            foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
            {
                foreach (CardRanks rank in Enum.GetValues(typeof(CardRanks)))
                {
                    cards.Add(new PlayingCard {Suit = suit, Rank = rank});
                }
            }
            return new CardsDeck {Cards = cards};
        }

        internal static CardsDeck NewShuffled()
        {
            var newDeck = New();
            newDeck.Cards.Shuffle();
            return newDeck;
        }
    }
}

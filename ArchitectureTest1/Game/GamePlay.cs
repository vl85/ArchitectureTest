using System.Collections.Generic;

namespace ArchitectureTest1.Game
{
    internal sealed class GamePlay
    {
        internal static GamePlayState NewState()
        {
            var newDeck = CardsDeck.NewShuffled();

            int currentIndex = 0;
            var mainColumns = new List<CardsMainColumn>();
            for (int i = 0; i < 6; i++)
            {
                var column = new CardsMainColumn();
                for (int j = 0; j < i + 1 && currentIndex < newDeck.Cards.Count; j++)
                {
                    column.AddFirst(newDeck.Cards[currentIndex++]);
                }
                mainColumns.Add(column);
            }

            var remainingCards = newDeck.Cards.GetRange(currentIndex, newDeck.Cards.Count - currentIndex);

            var homeColumns = new List<LinkedList<PlayingCard>>();
            for (int i = 0; i < 4; i++)
            {
                homeColumns.Add(new LinkedList<PlayingCard>());
            }

            var state = new GamePlayState
            {
                Deck = newDeck,
                MainColumns = mainColumns,
                Pile = new Pile(1, remainingCards),
                HomeColumns = homeColumns
            };

            return state;
        }
    }
}

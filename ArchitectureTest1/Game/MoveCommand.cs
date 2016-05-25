using System.Collections.Generic;

namespace ArchitectureTest1.Game
{
    internal class MoveCommand: ICommand
    {
        internal CardsMainColumn Previous { get; }
        internal CardsMainColumn Next { get; }
        internal PlayingCard Card { get; }

        public MoveCommand(CardsMainColumn previous, CardsMainColumn next, PlayingCard card)
        {
            Previous = previous;
            Next = next;
            Card = card;
        }

        public void Execute()
        {
            Next.AddFirst(Card);
        }

        public void Undo()
        {
            Next.RemoveFirst();
            Previous.AddFirst(Card);
        }
    }
}

using System.Collections.Generic;

namespace ComponentInterfaces
{
    public sealed class GameState
    {
        public GameState(IReadOnlyList<CardsMainColumnState> mainColumns, IReadOnlyList<MoveCommandState> executedCommands)
        {
            MainColumns = mainColumns;
            ExecutedCommands = executedCommands;
        }

        public IReadOnlyList<CardsMainColumnState> MainColumns { get; }

        public IReadOnlyList<MoveCommandState> ExecutedCommands { get; }
    }
}

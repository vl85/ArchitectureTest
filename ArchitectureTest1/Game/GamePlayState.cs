using System.Collections.Generic;
using ComponentInterfaces;

namespace ArchitectureTest1.Game
{
    internal sealed class GamePlayState
    {
        internal CardsDeck Deck { get; set; }

        internal List<CardsMainColumn> MainColumns { get; set; }

        internal List<LinkedList<PlayingCard>> HomeColumns { get; set; } 
        
        internal Pile Pile { get; set; }

        internal MovingCard MovingCard { get; set; }

        internal List<ICommand> ExecutedCommands { get; set; } = new List<ICommand>();

        internal Queue<ICommand> CommandsToExecute { get; set; } = new Queue<ICommand>();
    }
}

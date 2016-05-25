using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ComponentInterfaces;

namespace ArchitectureTest1.Game
{
    internal sealed class ComponentsController
    {
        private readonly List<IComponent> _components;

        internal ComponentsController(List<IComponent> components)
        {
            _components = components;
        }
        
        private Task _executingTask = Task.CompletedTask;
        private readonly object _lock = new object();
        internal void OnOnCommandExecuted(GamePlayState state)
        {
            if (_components == null || _components.Count == 0) return;

            var gameState = ConvertGamePlayState(state);

            lock (_lock)
            {
                _executingTask = _executingTask.ContinueWith(t =>
                {
                    foreach (var component in _components)
                    {
                        component.Execute(gameState);
                    }
                });
            }
        }

        private static GameState ConvertGamePlayState(GamePlayState state)
        {
            Func<PlayingCard, PlayingCardState> cardConvert = c => new PlayingCardState(c.Suit, c.Rank);
            Func<CardsMainColumn, CardsMainColumnState> columnConvert =
                col => new CardsMainColumnState(col.Cards.Select(cardConvert).ToArray());
            Func<MoveCommand, MoveCommandState> commandCovert =
                c => new MoveCommandState(columnConvert(c.Previous), columnConvert(c.Next), cardConvert(c.Card));
            var columns = state.MainColumns.Select(columnConvert).ToArray();
            var gameState = new GameState(columns,
                state.ExecutedCommands.OfType<MoveCommand>().Select(commandCovert).ToArray());
            return gameState;
        }
    }
}

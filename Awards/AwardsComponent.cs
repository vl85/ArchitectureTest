using System;
using ComponentInterfaces;

namespace Awards
{
    public class AwardsComponent : IComponent
    {
        private readonly AwardsData _awardsData;

        public AwardsComponent(AwardsData awardsData)
        {
            _awardsData = awardsData;
        }

        public void Execute(GameState gameState)
        {
            if (gameState.ExecutedCommands.Count < 2) return;

            var lastCommand = gameState.ExecutedCommands[gameState.ExecutedCommands.Count - 1];
            var previousCommand = gameState.ExecutedCommands[gameState.ExecutedCommands.Count - 2];
            if (Equals(lastCommand.Card, previousCommand.Card))
            {
                _awardsData.MainAwardReachedDateTime = DateTimeOffset.Now;
            }
        }
    }
}

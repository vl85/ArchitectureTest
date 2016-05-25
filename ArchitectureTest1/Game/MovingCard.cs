using Microsoft.Graphics.Canvas.UI.Xaml;

namespace ArchitectureTest1.Game
{
    internal sealed class MovingCard
    {
        internal PlayingCard PlayingCard { get; set; }

        internal CardsMainColumn SourceColumn { get; set; }

        internal void Update(GamePlayState state, PointerState pointerState)
        {
            PlayingCard.X = pointerState.Position.X - PlayingCard.Width / 2;
            PlayingCard.Y = pointerState.Position.Y - PlayingCard.Height / 4;
            if (!pointerState.IsTouched && pointerState.IsTouchChanged)
            {
                SourceColumn.AddFirst(PlayingCard);
                state.MovingCard = null;
            }
        }

        internal void Draw(CanvasAnimatedDrawEventArgs args)
        {
            PlayingCard.Draw(args);
        }
    }
}

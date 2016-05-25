using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace ArchitectureTest1.Game
{
    internal sealed class CardsMainColumn
    {
        internal LinkedList<PlayingCard> Cards { get; } = new LinkedList<PlayingCard>();

        internal double X { get; set; }

        internal double Y { get; set; }
        
        private const double DefaultYSpace = 30;
        
        internal void RemoveFirst()
        {
            Cards.RemoveFirst();
        }

        internal void AddFirst(PlayingCard card)
        {
            var rect = GetFirstRect();
            Cards.AddFirst(card);
            card.X = X;
            card.Y = rect.Y + DefaultYSpace;
        }

        internal Rect GetFirstRect()
        {
            return Cards.First != null
                ? new Rect(Cards.First.Value.X, Cards.First.Value.Y, PlayingCard.Width,
                    PlayingCard.Height)
                : new Rect(X, Y, PlayingCard.Width, PlayingCard.Height);
        }

        internal void Update(GamePlayState gamePlayState, PointerState pointerState)
        {
            double columnYOffset = Y;
            var current = Cards.Last;
            while (current != null)
            {
                if (gamePlayState.MovingCard == null || gamePlayState.MovingCard.PlayingCard != current.Value)
                {
                    current.Value.X = X;
                    current.Value.Y = columnYOffset;
                }
                current = current.Previous;
                columnYOffset += DefaultYSpace;
            }

            var rect = GetFirstRect();

            if (pointerState.IsTouched && pointerState.IsTouchChanged && gamePlayState.MovingCard == null && Cards.Count>0)
            {
                if (rect.Contains(pointerState.Position))
                {
                    gamePlayState.MovingCard = new MovingCard
                    {
                        PlayingCard = Cards.First.Value,
                        SourceColumn = this
                    };
                    Cards.RemoveFirst();
                }
            }
            else if (!pointerState.IsTouched && pointerState.IsTouchChanged && gamePlayState.MovingCard != null)
            {
                if (rect.Contains(pointerState.Position))
                {
                    var command = new MoveCommand(gamePlayState.MovingCard.SourceColumn, this,
                        gamePlayState.MovingCard.PlayingCard);
                    gamePlayState.CommandsToExecute.Enqueue(command);

                    gamePlayState.MovingCard = null;
                }
            }
        }

        internal void Draw(GamePlayState gamePlayState, CanvasAnimatedDrawEventArgs args)
        {
            var current = Cards.Last;
            while (current != null)
            {
                if (gamePlayState.MovingCard == null || gamePlayState.MovingCard.PlayingCard != current.Value)
                {
                    current.Value.Draw(args);
                }
                current = current.Previous;
            }
            if (Cards.Last == null)
            {
                args.DrawingSession.DrawRoundedRectangle(GetFirstRect(), 1, 1, Colors.Gray);
            }
        }
    }
}

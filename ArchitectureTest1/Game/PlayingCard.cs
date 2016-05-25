using Windows.Foundation;
using Windows.UI;
using ComponentInterfaces;
using Microsoft.Graphics.Canvas.Text;
using Microsoft.Graphics.Canvas.UI.Xaml;

namespace ArchitectureTest1.Game
{
    internal class PlayingCard
    {
        public CardSuits Suit { get; set; }
        public CardRanks Rank { get; set; }

        internal double X { get; set; }

        internal double Y { get; set; }
        
        internal const double Width = 120;

        internal const double Height = 160;

        internal const double TipWidth = 60;

        internal const double TipHeight = 30;

        private const double Padding = 1;

        internal void Draw(CanvasAnimatedDrawEventArgs args)
        {
            args.DrawingSession.DrawRoundedRectangle(new Rect(X, Y, Width, Height), 1, 1, Colors.Gray);
            args.DrawingSession.FillRoundedRectangle(
                new Rect(X + Padding, Y + Padding, Width - Padding, Height - Padding), 1, 1, Colors.AntiqueWhite);
            args.DrawingSession.DrawText(ToString(), new Rect(X + Padding*2, Y + Padding*2, TipWidth, TipHeight),
                Suit == CardSuits.Hearts || Suit == CardSuits.Diamonds ? Colors.Red : Colors.Black,
                new CanvasTextFormat());
        }

        public override string ToString()
        {
            int rankInt = (int) Rank;
            string rank = rankInt > 1 && rankInt <= 10
                ? rankInt.ToString()
                : Rank.ToString().Substring(0, 1);

            string suit = Suit.ToString().Substring(0, 1);

            return $"{rank} {suit}";
        }
    }
}

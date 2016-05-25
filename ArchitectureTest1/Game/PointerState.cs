using Windows.Foundation;

namespace ArchitectureTest1.Game
{
    internal sealed class PointerState
    {
        internal Point Position { get; set; }

        internal bool IsTouched { get; set; }

        internal bool IsTouchChanged { get; set; }
    }
}

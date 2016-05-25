using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace ArchitectureTest1.Game
{
    internal sealed class GameController
    {
        private Point _lastTouchedPoint;
        private bool _touched;
        private bool _previousIsTouched;

        internal PointerState GetPointerState()
        {
            return new PointerState
            {
                Position = _lastTouchedPoint,
                IsTouched = _touched,
                IsTouchChanged = _touched != _previousIsTouched
            };
        }

        internal void AfterUpdate()
        {
            _previousIsTouched = _touched;
        }

        internal void OnPointerPressed(Point position)
        {
            _lastTouchedPoint = position;
            _touched = true;
        }

        internal void OnPointerMoved(Point position)
        {
            _lastTouchedPoint = position;
        }

        internal void OnPointerReleased()
        {
            _touched = false;
        }
    }
}

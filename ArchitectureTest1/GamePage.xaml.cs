using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using ArchitectureTest1.Game;
using ComponentInterfaces;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Practices.ServiceLocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ArchitectureTest1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        private GamePlayState _state;
        private GameController _gameController;
        private ComponentsController _componentsController;

        public GamePage()
        {
            this.InitializeComponent();
        }

        private void CanvasAnimatedControl_OnUpdate(ICanvasAnimatedControl sender, CanvasAnimatedUpdateEventArgs args)
        {
            var pointerState = _gameController.GetPointerState();

            foreach (CardsMainColumn column in _state.MainColumns)
            {
                column.Update(_state, pointerState);
            }

            _state.MovingCard?.Update(_state, pointerState);

            while (_state.CommandsToExecute.Count > 0)
            {
                var command = _state.CommandsToExecute.Dequeue();
                command.Execute();
                _state.ExecutedCommands.Add(command);
                _componentsController?.OnOnCommandExecuted(_state);
            }

            _gameController.AfterUpdate();
        }

        private void CanvasAnimatedControl_OnDraw(ICanvasAnimatedControl sender, CanvasAnimatedDrawEventArgs args)
        {
            if (!_state.Pile.IsPileEmpty)
            {
                args.DrawingSession.DrawRoundedRectangle(new Rect(Layout.DefaultXSpace, Layout.DefaultYSpace, PlayingCard.Width, PlayingCard.Height), 1, 1, Colors.Gray);
            }

            foreach (PlayingCard card in _state.Pile.Faced)
            {
                card.Draw(args);
            }

            double homeColumnXOffset = Layout.DefaultXSpace * 4 + PlayingCard.Width*2;
            foreach (LinkedList<PlayingCard> column in _state.HomeColumns)
            {
                var element = column.First;
                if (element != null)
                {
                    element.Value.X = homeColumnXOffset;
                    element.Value.Y = Layout.DefaultYSpace;
                    element.Value.Draw(args);
                }
                else
                {
                    args.DrawingSession.DrawRoundedRectangle(
                        new Rect(homeColumnXOffset, Layout.DefaultYSpace, PlayingCard.Width, PlayingCard.Height), 1, 1, Colors.SeaGreen);
                }
                homeColumnXOffset += Layout.DefaultXSpace + PlayingCard.Width;
            }

            foreach (CardsMainColumn column in _state.MainColumns)
            {
                column.Draw(_state, args);
            }

            _state.MovingCard?.Draw(args);
        }

        private void GamePage_OnUnloaded(object sender, RoutedEventArgs e)
        {
            CanvasAnimatedControl.RemoveFromVisualTree();
            CanvasAnimatedControl = null;
        }

        private void CanvasAnimatedControl_OnGameLoopStarting(ICanvasAnimatedControl sender, object args)
        {
            if (_state == null)
            {
                _state = GamePlay.NewState();

                SetPileOffset();
                SetMainColumnsOffset();
            }
            if (_gameController == null)
            {
                _gameController = new GameController();
            }
            if (_componentsController == null)
            {
                _componentsController = new ComponentsController(ServiceLocator.Current.GetInstance<List<IComponent>>());
            }
        }

        private void SetPileOffset()
        {
            double pileXOffset = Layout.DefaultXSpace*2 + PlayingCard.Width;
            foreach (PlayingCard card in _state.Pile.Faced)
            {
                card.X = pileXOffset;
                card.Y = Layout.DefaultYSpace;
                pileXOffset += Layout.DefaultXSpace;
            }
        }

        private void SetMainColumnsOffset()
        {
            double mainColumnXOffset = Layout.DefaultXSpace;
            double columnYOffset = Layout.DefaultYSpace*2 + PlayingCard.Height;
            foreach (CardsMainColumn column in _state.MainColumns)
            {
                column.X = mainColumnXOffset;
                column.Y = columnYOffset;

                mainColumnXOffset += Layout.DefaultXSpace + PlayingCard.Width;
            }
        }

        private async void CanvasAnimatedControl_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            var position = e.GetCurrentPoint((UIElement) sender).Position;
            await CanvasAnimatedControl.RunOnGameLoopThreadAsync(() => _gameController.OnPointerPressed(position));
        }

        private async void CanvasAnimatedControl_OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var position = e.GetCurrentPoint((UIElement)sender).Position;
            await CanvasAnimatedControl.RunOnGameLoopThreadAsync(() => _gameController.OnPointerMoved(position));
        }

        private async void CanvasAnimatedControl_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            await CanvasAnimatedControl.RunOnGameLoopThreadAsync(() => _gameController.OnPointerReleased());
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var rootFrame = (Frame)Window.Current.Content;

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                rootFrame.CanGoBack
                    ? AppViewBackButtonVisibility.Visible
                    : AppViewBackButtonVisibility.Collapsed;
        }
    }
}

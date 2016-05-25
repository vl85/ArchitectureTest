using Awards;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;

namespace ArchitectureTest1
{
    internal sealed class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public RelayCommand GoToPlayingCards { get; }
        public RelayCommand GoToAwards { get; }

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            GoToPlayingCards = new RelayCommand(()=>_navigationService.NavigateTo(nameof(GamePage)));
            GoToAwards = new RelayCommand(()=>_navigationService.NavigateTo(nameof(AwardsPage)));
        }
    }
}

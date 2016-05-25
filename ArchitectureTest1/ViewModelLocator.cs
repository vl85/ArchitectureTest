using Awards;
using Microsoft.Practices.ServiceLocation;

namespace ArchitectureTest1
{
    internal class ViewModelLocator
    {
        public MainPageViewModel MainPageViewModel => ServiceLocator.Current.GetInstance<MainPageViewModel>();

        public AwardsPageViewModel AwardsPageViewModel => ServiceLocator.Current.GetInstance<AwardsPageViewModel>();
    }
}

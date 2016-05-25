using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Awards
{
    public sealed class AwardsData : INotifyPropertyChanged
    {
        private DateTimeOffset? _mainAwardReachedDateTime;
        public DateTimeOffset? MainAwardReachedDateTime
        {
            get { return _mainAwardReachedDateTime; }
            set
            {
                if (_mainAwardReachedDateTime != value)
                {
                    _mainAwardReachedDateTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _someCloudAwardProgress;
        public int SomeCloudAwardProgress
        {
            get { return _someCloudAwardProgress; }
            set
            {
                if (_someCloudAwardProgress != value)
                {
                    _someCloudAwardProgress = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

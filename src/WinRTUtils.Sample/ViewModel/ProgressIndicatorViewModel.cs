using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace WinRTUtils.Sample.ViewModel
{
    public class ProgressIndicatorViewModel : ViewModelBase
    {
        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                Set(() => IsVisible, ref _isVisible, value);

                if (ShowIndicatorCommand != null)
                {
                    ShowIndicatorCommand.RaiseCanExecuteChanged();
                }

                if (HideIndicatorCommand != null)
                {
                    HideIndicatorCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private string _text;

        public string Text
        {
            get { return _text; }
            set { Set(() => Text, ref _text, value); }
        }

        private double _progressValue = 0;

        public double ProgressValue
        {
            get { return _progressValue; }
            set
            {
                Set(() => ProgressValue, ref _progressValue, value);

                if (SetProgressToNullCommand != null)
                {
                    SetProgressToNullCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand ShowIndicatorCommand { get; private set; }

        public RelayCommand HideIndicatorCommand { get; private set; }

        public RelayCommand SetProgressToNullCommand { get; private set; }

        public ProgressIndicatorViewModel()
        {
            ShowIndicatorCommand = new RelayCommand(() => IsVisible = true, () => !IsVisible);
            HideIndicatorCommand = new RelayCommand(() => IsVisible = false, () => IsVisible);
        }
    }
}

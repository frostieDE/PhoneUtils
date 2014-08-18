using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneUtils.Sample.Messages;

namespace PhoneUtils.Sample.ViewModel
{
    public class BehaviorsViewModel : ViewModelBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set { Set(() => Text, ref _text, value); }
        }

        public RelayCommand SearchCommand { get; private set; }

        public IMessenger Messenger { get { return base.MessengerInstance; } }

        public BehaviorsViewModel(IMessenger messenger) : base(messenger)
        {
            SearchCommand = new RelayCommand(Search);
        }

        private void Search()
        {
            Messenger.Send<SampleMessage>(new SampleMessage());
        }
    }
}

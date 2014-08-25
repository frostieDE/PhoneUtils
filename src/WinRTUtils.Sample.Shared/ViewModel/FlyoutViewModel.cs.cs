using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using WinRTUtils.Sample.Messages;

namespace WinRTUtils.Sample.ViewModel
{
    public class FlyoutViewModel : ViewModelBase
    {
        public List<string> Items { get; private set; }

        public RelayCommand<string> ItemCommand { get; private set; }

        public IMessenger Messenger { get { return base.MessengerInstance; } }

        public FlyoutViewModel(IMessenger messenger) : base(messenger)
        {
            Items = new List<string>();
            Items.Add("item 1");
            Items.Add("item 2");
            Items.Add("item 3");

            ItemCommand = new RelayCommand<string>(s =>
            {
                Messenger.Send<SampleMessageWithItem>(new SampleMessageWithItem { Item = s });
            });
        }
    }
}

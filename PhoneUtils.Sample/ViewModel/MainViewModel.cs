using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PhoneUtils.Collections;
using PhoneUtils.Messages;
using System;

namespace PhoneUtils.Sample.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public SortedObservableCollection<ShowCase> ShowCases { get; private set; }

        public RelayCommand<ShowCase> OpenShowCaseCommand { get; private set; }

        public IMessenger Messenger { get { return base.MessengerInstance; } }

        public MainViewModel(IMessenger messenger) : base(messenger)
        {
            ShowCases = new SortedObservableCollection<ShowCase>();
            ShowCases.Add(new ShowCase { Title = "SortedObservableCollection", Description = "Show how to use the SortedObservableCollection", TargetPage = "SortedView" });

            OpenShowCaseCommand = new RelayCommand<ShowCase>(sc => Messenger.Send<NavigateMessage>(new NavigateMessage { TargetPage = sc.TargetPage }));
        }

        public class ShowCase : ObservableObject, IComparable<ShowCase>
        {
            private string _title;

            public string Title
            {
                get { return _title; }
                set { Set(() => Title, ref _title, value); }
            }

            private string _description;

            public string Description
            {
                get { return _description; }
                set { Set(() => Description, ref _description, value); }
            }

            private string _targetPage;

            public string TargetPage
            {
                get { return _targetPage; }
                set { Set(() => TargetPage, ref _targetPage, value); }
            }

            public int CompareTo(ShowCase other)
            {
                return Title.CompareTo(other.Title);
            }
        }
    }
}
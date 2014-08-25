using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WinRTUtils.Collections;
using WinRTUtils.Messages;
using System;

namespace WinRTUtils.Sample.ViewModel
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
            ShowCases.Add(new ShowCase { Title = "TextBox Behaviors", Description = "Show how to use the behaviors for the TextBox control", TargetPage = "BehaviorsView" });
            ShowCases.Add(new ShowCase { Title = "ProgressIndicator Behavior", Description = "Show how to use the behaviors for progress indicator", TargetPage = "ProgressIndicatorView" });

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
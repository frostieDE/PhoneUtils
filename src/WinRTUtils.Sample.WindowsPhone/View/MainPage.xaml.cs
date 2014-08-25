using WinRTUtils.Messages;
using WinRTUtils.Sample.Common;
using WinRTUtils.Sample.ViewModel;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WinRTUtils.Sample.View
{
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;

        public MainPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            var vm = (MainViewModel)DataContext;

            Loaded += (s, e) =>
            {
                vm.Messenger.Register<NavigateMessage>(this, msg =>
                {
                    Frame.Navigate(Type.GetType("WinRTUtils.Sample.View." + msg.TargetPage), msg.Parameter);
                });
            };

            Unloaded += (s, e) =>
            {
                vm.Messenger.Unregister(this);
            };
        }

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper-Registration

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}

using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTUtils.Messages;
using WinRTUtils.Sample.Common;
using WinRTUtils.Sample.ViewModel;

namespace WinRTUtils.Sample.View
{
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);

            var vm = (MainViewModel)DataContext;

            Loaded += (s, e) =>
            {
                vm.Messenger.Register<NavigateMessage>(this, msg =>
                {
                    try
                    {
                        Frame.Navigate(Type.GetType("WinRTUtils.Sample.View." + msg.TargetPage), msg.Parameter);
                    }
                    catch { }
                });
            };

            Unloaded += (s, e) =>
            {
                vm.Messenger.Unregister(this);
            };
        }

        #region NavigationHelper

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}

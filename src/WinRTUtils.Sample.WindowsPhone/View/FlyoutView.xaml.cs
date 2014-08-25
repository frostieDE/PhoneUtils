using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTUtils.Sample.Common;
using WinRTUtils.Sample.Messages;
using WinRTUtils.Sample.ViewModel;

namespace WinRTUtils.Sample.View
{
    public sealed partial class FlyoutView : Page
    {
        private NavigationHelper navigationHelper;

        public FlyoutView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);

            var vm = (FlyoutViewModel)DataContext;

            Loaded += (s, e) =>
            {
                vm.Messenger.Register<SampleMessageWithItem>(this, async msg =>
                {
                    var dialog = new MessageDialog("Selected item: " + msg.Item, "command executed");
                    await dialog.ShowAsync();
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

        #region NavigationHelper

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

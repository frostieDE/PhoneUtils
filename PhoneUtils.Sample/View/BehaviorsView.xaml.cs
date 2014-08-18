using PhoneUtils.Sample.Common;
using PhoneUtils.Sample.Messages;
using PhoneUtils.Sample.ViewModel;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PhoneUtils.Sample.View
{
    public sealed partial class BehaviorsView : Page
    {
        private NavigationHelper navigationHelper;

        public BehaviorsView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);

            var vm = (BehaviorsViewModel)DataContext;

            Loaded += (s, e) =>
            {
                vm.Messenger.Register<SampleMessage>(this, async msg =>
                {
                    var dialog = new MessageDialog("search button pressed", "command execution");
                    await dialog.ShowAsync();
                });
            };

            Unloaded += (s, e) =>
            {
                vm.Messenger.Unregister(this);
            };

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }
    }
}

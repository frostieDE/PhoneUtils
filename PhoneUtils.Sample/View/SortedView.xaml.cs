using PhoneUtils.Sample.Common;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace PhoneUtils.Sample.View
{
    public sealed partial class SortedView : Page
    {
        private NavigationHelper navigationHelper;

        public SortedView()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
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

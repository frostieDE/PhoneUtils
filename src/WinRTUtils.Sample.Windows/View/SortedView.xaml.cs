using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WinRTUtils.Sample.Common;

namespace WinRTUtils.Sample.View
{
    public sealed partial class SortedView : Page
    {
        private NavigationHelper navigationHelper;

        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        public SortedView()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
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

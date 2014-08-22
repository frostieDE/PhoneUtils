using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace WinRTUtils.Actions
{
    public class OpenFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            var element = sender as FrameworkElement;
            var flyout = FlyoutBase.GetAttachedFlyout(element);

            if (flyout != null)
            {
                flyout.ShowAt(element);
            }

            return null;
        }
    }
}

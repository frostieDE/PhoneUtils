using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinRTUtils.Behaviors
{
    [TypeConstraint(typeof(TextBox))]
    public class FocusTextboxBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            var textbox = AssociatedObject as TextBox;

            if (textbox != null)
            {
                textbox.Loaded += textbox_Loaded;
            }
        }

        private void textbox_Loaded(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                textbox.Focus(FocusState.Keyboard);
            }
        }

        public void Detach()
        {
            var textbox = AssociatedObject as TextBox;

            if (textbox != null)
            {
                textbox.Loaded -= textbox_Loaded;
            }
        }
    }
}

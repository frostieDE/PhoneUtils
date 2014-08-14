using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhoneUtils.Behaviors
{
    [TypeConstraint(typeof(TextBox))]
    public class BindTextboxBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            var textbox = AssociatedObject as TextBox;

            if (textbox != null)
            {
                textbox.TextChanged += textbox_TextChanged;
            }
        }

        private void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox != null)
            {
                var bi = textbox.GetBindingExpression(TextBox.TextProperty);

                if (bi != null)
                {
                    bi.UpdateSource();
                }
            }
        }

        public void Detach()
        {
            var textbox = AssociatedObject as TextBox;

            if (textbox != null)
            {
                textbox.TextChanged -= textbox_TextChanged;
            }
        }
    }
}

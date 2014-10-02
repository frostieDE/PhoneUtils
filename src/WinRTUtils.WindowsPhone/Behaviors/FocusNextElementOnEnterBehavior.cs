using Microsoft.Xaml.Interactivity;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinRTUtils.Behaviors
{
    public class FocusNextElementOnEnterBehavior : DependencyObject, IBehavior
    {
        #region Dependency Properties

        public static readonly DependencyProperty NextElementProperty = DependencyProperty.Register("NextElement", typeof(DependencyObject), typeof(FocusNextElementOnEnterBehavior), null);

        /// <summary>
        /// Gets or sets the next element
        /// which is to be focused after
        /// the user enters 'Enter' in
        /// the attached element
        /// </summary>
        public DependencyObject NextElement
        {
            get { return (DependencyObject)GetValue(NextElementProperty); }
            set { SetValue(NextElementProperty, value); }
        }

        #endregion

        private readonly BindKeyToCommandBehavior BindKeyToCommandBehavior = new BindKeyToCommandBehavior();

        public FocusNextElementOnEnterBehavior()
        {
            BindKeyToCommandBehavior.Key = Windows.System.VirtualKey.Enter;
            BindKeyToCommandBehavior.KeyEvent = KeyEventName.KeyUp;
            BindKeyToCommandBehavior.Command = new FocusNextElementCommand(this);
        }

        #region IBehavior

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            BindKeyToCommandBehavior.Attach(associatedObject);
        }

        public void Detach()
        {
            BindKeyToCommandBehavior.Detach();
        }

        #endregion

        #region Helper command

        private class FocusNextElementCommand : ICommand
        {
            private FocusNextElementOnEnterBehavior Behavior = null;

            public FocusNextElementCommand(FocusNextElementOnEnterBehavior behavior)
            {
                Behavior = behavior;
            }

            public bool CanExecute(object parameter)
            {
                return Behavior.NextElement != null && (Behavior.NextElement is TextBox || Behavior.NextElement is PasswordBox);
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                var textbox = Behavior.NextElement as TextBox;
                var passwordbox = Behavior.NextElement as PasswordBox;

                if (textbox != null)
                {
                    textbox.Focus(FocusState.Keyboard);
                }
                else if (passwordbox != null)
                {
                    passwordbox.Focus(FocusState.Keyboard);
                }
            }
        }

        #endregion
    }
}

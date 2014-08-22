using Microsoft.Xaml.Interactivity;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace WinRTUtils.Behaviors
{
    public class BindKeyToCommandBehavior : DependencyObject, IBehavior
    {
        #region Dependency properties

        public static DependencyProperty KeyEventProperty = DependencyProperty.Register("KeyEvent", typeof(KeyEventName), typeof(BindKeyToCommandBehavior), new PropertyMetadata(KeyEventName.KeyDown, OnKeyEventChanged));

        private static void OnKeyEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as BindKeyToCommandBehavior).OnKeyEventChanged(e);
        }

        private void OnKeyEventChanged(DependencyPropertyChangedEventArgs e)
        {
            UnregisterEvent((KeyEventName)e.OldValue);
            RegisterEvent((KeyEventName)e.NewValue);
        }

        /// <summary>
        /// Gets or sets the type of event
        /// that this behavior should listen to
        /// </summary>
        public KeyEventName KeyEvent
        {
            get { return (KeyEventName)GetValue(KeyEventProperty); }
            set { SetValue(KeyEventProperty, value); }
        }

        public static DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof(VirtualKey), typeof(BindKeyToCommandBehavior), new PropertyMetadata(VirtualKey.Enter));

        /// <summary>
        /// Gets or sets the key that must be
        /// pressed in order to execute the
        /// given command
        /// </summary>
        public VirtualKey Key
        {
            get { return (VirtualKey)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }


        public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(BindKeyToCommandBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command that is executed
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(BindKeyToCommandBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command parameter
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion

        #region Event registration

        private void RegisterEvent(KeyEventName keyEvent)
        {
            var textbox = AssociatedObject as TextBox;
            var passwordbox = AssociatedObject as PasswordBox;

            switch (keyEvent)
            {
                case KeyEventName.KeyDown:

                    if (textbox != null)
                    {
                        textbox.KeyDown += OnKeyDown;
                    }
                    else if (passwordbox != null)
                    {
                        passwordbox.KeyDown += OnKeyDown;
                    }

                    break;

                case KeyEventName.KeyUp:

                    if (textbox != null)
                    {
                        textbox.KeyUp += OnKeyUp;
                    }
                    else if (passwordbox != null)
                    {
                        passwordbox.KeyUp += OnKeyUp;
                    }

                    break;
            }
        }

        private void UnregisterEvent(KeyEventName keyEvent)
        {
            var textbox = AssociatedObject as TextBox;
            var passwordbox = AssociatedObject as PasswordBox;

            switch (keyEvent)
            {
                case KeyEventName.KeyDown:

                    if (textbox != null)
                    {
                        textbox.KeyDown -= OnKeyDown;
                    }
                    else if (passwordbox != null)
                    {
                        passwordbox.KeyDown -= OnKeyDown;
                    }

                    break;

                case KeyEventName.KeyUp:

                    if (textbox != null)
                    {
                        textbox.KeyUp -= OnKeyUp;
                    }
                    else if (passwordbox != null)
                    {
                        passwordbox.KeyUp -= OnKeyUp;
                    }

                    break;
            }
        }    

        #endregion

        #region IBehavior

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            RegisterEvent(KeyEvent);
        }

        public void Detach()
        {
            UnregisterEvent(KeyEvent);
        }

        #endregion

        #region Events

        private void OnKeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (Command != null && e.Key == Key)
            {
                if (Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (Command != null && e.Key == Key)
            {
                if (Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
            }
        }

        #endregion
    }

    public enum KeyEventName
    {
        KeyDown,
        KeyUp
    }
}

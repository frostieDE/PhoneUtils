#if WINDOWS_PHONE_APP
using Microsoft.Xaml.Interactivity;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WinRTUtils.Behaviors
{
    public abstract class ProgressIndicatorBehaviorBase : DependencyObject, IBehavior
    {
        /// <summary>
        /// Reference to the current
        /// StatusBar
        /// </summary>
        protected StatusBar _statusbar = null;

        public static DependencyProperty IsVisibleProperty = DependencyProperty.Register("IsVisible", typeof(bool), typeof(ProgressIndicatorBehaviorBase), new PropertyMetadata(false, OnIsVisibleChanged));

        private static void OnIsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ProgressIndicatorBehaviorBase).OnIsVisibleChanged(e);
        }

        private void OnIsVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                ShowProgressIndicator();
            }
            else
            {
                HideProgressIndicator();
            }
        }

        /// <summary>
        /// Gets or sets whether the
        /// ProgressIndicator is visible
        /// </summary>
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        private static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ProgressIndicatorBehaviorBase), new PropertyMetadata(string.Empty, OnTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ProgressIndicatorBehaviorBase).OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_statusbar != null && _statusbar.ProgressIndicator != null)
            {
                _statusbar.ProgressIndicator.Text = (string)e.NewValue;
            }
        }

        /// <summary>
        /// Gets or sets the text of the
        /// ProgressIndicator
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private double? _progressValue = null;

        /// <summary>
        /// Gets or sets the progress value
        /// </summary>
        protected double? ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;

                if (_statusbar != null && _statusbar.ProgressIndicator != null)
                {
                    _statusbar.ProgressIndicator.ProgressValue = _progressValue;
                }
            }
        }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            var page = AssociatedObject as Page;

            if (page != null)
            {
                _statusbar = StatusBar.GetForCurrentView();

                page.Loaded += OnPageLoaded;
                page.Unloaded += OnPageUnloaded;
            }
        }

        public void Detach()
        {
            var page = AssociatedObject as Page;

            if (page != null)
            {
                _statusbar = null;

                page.Loaded -= OnPageLoaded;
                page.Unloaded -= OnPageUnloaded;
            }
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            var page = sender as Page;

            if (page != null && page.Frame != null)
            {
                page.Frame.Navigating -= OnNavigating;
            }
        }

        protected void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            var page = sender as Page;

            if (page != null && page.Frame != null)
            {
                page.Frame.Navigating += OnNavigating;
            }

            // restore data
            if (_statusbar != null && _statusbar.ProgressIndicator != null)
            {
                _statusbar.ProgressIndicator.Text = Text;
                _statusbar.ProgressIndicator.ProgressValue = ProgressValue;
            }

            if (IsVisible)
            {
                ShowProgressIndicator();
            }
        }

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            var page = AssociatedObject as Page;

            if(page == null) {
                return;
            }

            if ((e.NavigationMode == NavigationMode.New && page.GetType() != e.SourcePageType) ||
                (e.NavigationMode == NavigationMode.Back && page.GetType() != e.SourcePageType))
            {
                HideProgressIndicator();
            }
        }

        protected async void ShowProgressIndicator()
        {
            if (_statusbar == null || _statusbar.ProgressIndicator == null)
            {
                return;
            }

            await _statusbar.ProgressIndicator.ShowAsync();
        }

        private async void HideProgressIndicator()
        {
            if (_statusbar == null || _statusbar.ProgressIndicator == null)
            {
                return;
            }

            await _statusbar.ProgressIndicator.HideAsync();
        }
    }

    [TypeConstraint(typeof(Page))]
    public class ProgressIndicatorBehavior : ProgressIndicatorBehaviorBase
    {
        public static DependencyProperty ProgressValueProperty = DependencyProperty.Register("ProgressValue", typeof(double), typeof(ProgressIndicatorBehavior), new PropertyMetadata(null, OnProgressValueChanged));

        private static void OnProgressValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ProgressIndicatorBehavior).OnProgressValueChanged(e);
        }

        private void OnProgressValueChanged(DependencyPropertyChangedEventArgs e)
        {
            base.ProgressValue = e.NewValue as double?;
        }

        /// <summary>
        /// Gets or sets the value of the
        /// ProgressIndicator
        /// </summary>
        public new double ProgressValue
        {
            get { return (double)GetValue(ProgressValueProperty); }
            set { SetValue(ProgressValueProperty, value); }
        }
    }

    [TypeConstraint(typeof(Page))]
    public class IndeterminateProgressIndicatorBehavior : ProgressIndicatorBehaviorBase
    {
        public IndeterminateProgressIndicatorBehavior()
        {
            ProgressValue = null;
        }
    }
}
#endif
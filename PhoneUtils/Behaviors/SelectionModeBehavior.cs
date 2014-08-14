using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PhoneUtils.Behaviors
{
    [TypeConstraint(typeof(ListViewBase))]
    public class SelectionModeBehavior : DependencyObject, IBehavior
    {
        public static DependencyProperty SelectionModeProperty = DependencyProperty.Register("SelectionMode", typeof(ListViewSelectionMode), typeof(SelectionModeBehavior), new PropertyMetadata(ListViewSelectionMode.Multiple, OnSelectionModeChanged));

        private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SelectionModeBehavior).OnSelectionModeChanged(e);
        }

        private void OnSelectionModeChanged(DependencyPropertyChangedEventArgs e)
        {
            var listview = AssociatedObject as ListViewBase;

            if (listview != null && IsSelectionEnabled)
            {
                listview.SelectionMode = SelectionMode;
            }
        }

        /// <summary>
        /// Gets or sets the SelectionMode
        /// property for the ListView/GridView
        /// control
        /// </summary>
        public ListViewSelectionMode SelectionMode
        {
            get { return (ListViewSelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static DependencyProperty IsSelectionEnabledProperty = DependencyProperty.Register("IsSelectionEnabled", typeof(bool), typeof(SelectionModeBehavior), new PropertyMetadata(false, OnIsSelectionEnabledChanged));

        private static void OnIsSelectionEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SelectionModeBehavior).OnIsSelectionEnabledChanged(e);
        }

        private void OnIsSelectionEnabledChanged(DependencyPropertyChangedEventArgs e)
        {
            var listview = AssociatedObject as ListViewBase;

            if (listview != null)
            {
                listview.SelectionMode = IsSelectionEnabled ? SelectionMode : ListViewSelectionMode.None;

                if (listview.SelectionMode == ListViewSelectionMode.None)
                {
                    listview.IsItemClickEnabled = IsItemsClickEnabled;
                }
                else
                {
                    IsItemsClickEnabled = listview.IsItemClickEnabled;
                    listview.IsItemClickEnabled = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the specified
        /// SelectionMode is currently enabled
        /// </summary>
        public bool IsSelectionEnabled
        {
            get { return (bool)GetValue(IsSelectionEnabledProperty); }
            set { SetValue(IsSelectionEnabledProperty, value); }
        }

        /// <summary>
        /// Saves the IsItemsClickEnabled property
        /// </summary>
        private bool IsItemsClickEnabled { get; set; }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
        }

        public void Detach()
        {
            AssociatedObject = null;
        }
    }
}

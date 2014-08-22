using Microsoft.Xaml.Interactivity;
using System.Collections;
using System.Collections.Specialized;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WinRTUtils.Behaviors
{
    public class UpdateSelectedItemsBehavior : DependencyObject, IBehavior
    {
        public static DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IList), typeof(UpdateSelectedItemsBehavior), new PropertyMetadata(null));

        public IList SelectedItems
        {
            get { return (IList)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;

            var view = AssociatedObject as ListViewBase;

            if (view != null)
            {
                view.SelectionChanged += view_SelectionChanged;
            }
        }

        public void Detach()
        {
            var view = AssociatedObject as ListViewBase;

            if (view != null)
            {
                view.SelectionChanged -= view_SelectionChanged;
            }
        }

        private void view_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedItems == null || !(AssociatedObject is ListViewBase))
            {
                return;
            }

            var view = AssociatedObject as ListViewBase;

            if (e.AddedItems != null)
            {
                foreach (var item in e.AddedItems)
                {
                    if (view.Items.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }

            if (e.RemovedItems != null)
            {
                foreach (var item in e.RemovedItems)
                {
                    if (view.Items.Contains(item))
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }
    }
}

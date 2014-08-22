using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WinRTUtils.Collections
{
    /// <summary>
    /// An RangeObservableCollection<typeparamref name="T" /> which is sorted.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortedObservableCollection<T> : ObservableCollection<T> where T : class, IComparable<T>
    {
        /// <summary>
        /// Gets or sets the sort order
        /// </summary>
        public SortOrderType SortOrder = SortOrderType.Ascending;

        public enum SortOrderType
        {
            Ascending, Descending
        };

        /// <summary>
        /// Gets or sets a function which
        /// returns whether an item has changed
        /// so its position should be updated.
        /// 
        /// Argument: name of the property that has
        /// changed.
        /// 
        /// This requires T to be INotifyPropertyChanged
        /// </summary>
        public Func<string, bool> ItemHasChanged = null;

        public SortedObservableCollection()
        {
            CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (T item in e.NewItems)
                    {
                        if (item is INotifyPropertyChanged)
                        {
                            (item as INotifyPropertyChanged).PropertyChanged += SortedObservableCollection_PropertyChanged;
                        }
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (T item in e.OldItems)
                    {
                        if (item is INotifyPropertyChanged)
                        {
                            (item as INotifyPropertyChanged).PropertyChanged -= SortedObservableCollection_PropertyChanged;
                        }
                    }
                }
            };
        }

        private void SortedObservableCollection_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ItemHasChanged != null && ItemHasChanged(e.PropertyName))
            {
                var item = sender as T;

                if (item != null)
                {
                    var index = IndexOf(item);
                    var indexComp = index + 1;

                    if (indexComp < Count)
                    {
                        var itemComp = this[indexComp];
                        var comp = item.CompareTo(itemComp);

                        if (SortOrder == SortOrderType.Ascending && comp == -1 ||
                           SortOrder == SortOrderType.Descending && comp == 1)
                        {
                            Remove(item);
                            Add(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of a sorted insetion - O(n)
        /// Source: http://www.xamlplayground.org/post/2010/04/27/Keeping-an-ObservableCollection-sorted-with-a-method-override.aspx
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        protected override void InsertItem(int index, T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                switch (Math.Sign(this[i].CompareTo(item)))
                {
                    case 1:
                        if (SortOrder == SortOrderType.Ascending)
                        {
                            base.InsertItem(i, item);
                            return;
                        }
                        break;

                    case -1:
                        if (SortOrder == SortOrderType.Descending)
                        {
                            base.InsertItem(i, item);
                            return;
                        }
                        break;

                    case 0:
                        base.InsertItem(i, item);
                        return;

                }
            }

            base.InsertItem(this.Count, item);
        }
    }
}

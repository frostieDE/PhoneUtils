using System;
using System.Collections.Generic;
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
            : base()
        {
            BindEvents();
        }

        public SortedObservableCollection(IEnumerable<T> collection)
            : this()
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        private void BindEvents()
        {
            CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (T item in e.NewItems)
                    {
                        if (item is INotifyPropertyChanged)
                        {
                            (item as INotifyPropertyChanged).PropertyChanged += OnItemPropertyChanged;
                        }
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (T item in e.OldItems)
                    {
                        if (item is INotifyPropertyChanged)
                        {
                            (item as INotifyPropertyChanged).PropertyChanged -= OnItemPropertyChanged;
                        }
                    }
                }
            };
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ItemHasChanged != null && ItemHasChanged(e.PropertyName))
            {
                var item = sender as T;

                if (item != null)
                {
                    var index = IndexOf(item);

                    var indexPrev = index - 1;

                    // Test previous item in collection
                    if (indexPrev >= 0)
                    {
                        var comp = Math.Sign(item.CompareTo(this[indexPrev]));

                        if ((SortOrder == SortOrderType.Ascending && comp == -1) ||
                            (SortOrder == SortOrderType.Descending && comp == 1))
                        {
                            Rearrange(item);

                            // no need to test next item as we have to rearrange
                            return; 
                        }
                    }

                    // Text next item in collection
                    var indexNext = index + 1;

                    if (indexNext < Count)
                    {
                        var comp = Math.Sign(item.CompareTo(this[indexNext]));

                        if ((SortOrder == SortOrderType.Ascending && comp == 1) ||
                            (SortOrder == SortOrderType.Descending && comp == -1))
                        {
                            Rearrange(item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Moves an item to the correct
        /// position inside the list
        /// </summary>
        /// <param name="item"></param>
        private void Rearrange(T item)
        {
            var oldIndex = IndexOf(item);
            int newIndex = oldIndex;

            var stopLoop = false;

            for (int i = 0; i < this.Count && !stopLoop; i++)
            {
                if(i == oldIndex) {
                    continue;
                }

                var compResult = Math.Sign(this[i].CompareTo(item));

                if (compResult == 0 ||
                    (compResult == 1 && SortOrder == SortOrderType.Ascending) ||
                    (compResult == -1 && SortOrder == SortOrderType.Descending))
                {
                    newIndex = i;
                    break;
                }
            }

            if (oldIndex == newIndex)
            {
                /**
                 * We did not find any item that is "less"
                 * or "greater" than the item which is to be
                 * moved
                 * -> put it at the top/bottom of the collection
                 */
                if (SortOrder == SortOrderType.Ascending)
                {
                    newIndex = this.Count - 1;
                }
                else
                {
                    newIndex = 0;
                }
            }

            if (oldIndex != newIndex)
            {
                Move(oldIndex, newIndex);
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

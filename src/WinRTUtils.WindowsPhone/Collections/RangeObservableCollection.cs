using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace WinRTUtils.Collections
{
    /// <summary>
    /// An ObservableCollection which adds 
    /// methods to add or remove ranges of
    /// items.
    /// 
    /// All methods will call OnCollectionChanged
    /// for the whole range of added/removed items
    /// instead of each item individually.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        public RangeObservableCollection()
            : base() { }

        public RangeObservableCollection(IEnumerable<T> collection)
            : base(collection)
        { }

        /// <summary>
        /// Adds a range of items to the collection.
        /// </summary>
        /// <param name="items">Items to be added</param>
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items must not be null");
            }

            CheckReentrancy();

            foreach (var item in items)
            {
                Items.Add(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Items[]"));
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>(items)));
        }
        
        /// <summary>
        /// Removes a range of items from the collection.
        /// </summary>
        /// <param name="items">Items to be removed</param>
        public void RemoveRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items must not be null");
            }

            if (items.Count() == 0)
            {
                return;
            }

            foreach (var item in items)
            {
                Items.Remove(item);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Items[]"));
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<T>(items)));
        }

        /// <summary>
        /// Removes specified items from the collection.
        /// </summary>
        /// <param name="predicate">Function which determines whether an item will be deleted</param>
        public void RemoveAll(Func<T, bool> predicate)
        {
            RemoveRange(Items.Where(predicate).ToList());
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PhoneUtils.Collections;
using System.Collections.ObjectModel;

namespace PhoneUtils.Sample.ViewModel
{
    public class SortedViewModel : ViewModelBase
    {
        public SortedObservableCollection<string> Items { get; private set; }

        public RelayCommand AddCommand { get; private set; }

        private string _newItem;

        public string NewItem
        {
            get { return _newItem; }
            set
            {
                Set(() => NewItem, ref _newItem, value);

                if (AddCommand != null)
                {
                    AddCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isSelectionEnabled = false;

        public bool IsSelectionEnabled
        {
            get { return _isSelectionEnabled; }
            set { Set(() => IsSelectionEnabled, ref _isSelectionEnabled, value); }
        }

        public RelayCommand ToggleSelectionCommand { get; private set; }

        public RelayCommand RemoveSelectionCommand { get; private set; }

        public ObservableCollection<string> SelectedItems { get; private set; }

        public SortedViewModel()
        {
            Items = new SortedObservableCollection<string>();
            SelectedItems = new ObservableCollection<string>();
            SelectedItems.CollectionChanged += (s, e) =>
            {
                if (SelectedItems.Count == 0)
                {
                    IsSelectionEnabled = false;
                }

                if(RemoveSelectionCommand != null)
                {
                    RemoveSelectionCommand.RaiseCanExecuteChanged();
                }
            };

            AddCommand = new RelayCommand(() =>
            {
                Items.Add(NewItem);
                NewItem = string.Empty;
            }, () => !string.IsNullOrWhiteSpace(NewItem));

            ToggleSelectionCommand = new RelayCommand(() => IsSelectionEnabled = !IsSelectionEnabled);

            RemoveSelectionCommand = new RelayCommand(() =>
            {
                foreach (var item in SelectedItems)
                {
                    Items.Remove(item);
                }

                SelectedItems.Clear();
            }, () => SelectedItems.Count > 0);
        }
    }
}

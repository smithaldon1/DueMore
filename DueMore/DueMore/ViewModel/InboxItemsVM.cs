using DueMore.DAL;
using DueMore.Model;
using DueMore.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace DueMore.ViewModel
{
    public class InboxItemsVM : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private string notes;
        public string Notes
        {
            get
            {
                return notes;
            }
            set
            {
                notes = value;
                OnPropertyChanged("Notes");
            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }
            set
            {
                dueDate = value;
                OnPropertyChanged("DueDate");
            }
        }

        private InboxItem selectedInboxItem;
        public InboxItem SelectedInboxItem
        {
            get
            {
                return selectedInboxItem;
            }
            set
            {
                selectedInboxItem = value;
                OnPropertyChanged("SelectedInboxItem");
                if (selectedInboxItem != null)
                    App.Current.MainPage.Navigation.PushAsync(new InboxDetailView());
            }
        }

        public ObservableCollection<InboxItem> InboxItems { get; set; }
        public ICommand SaveInboxItemCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InboxItemsVM()
        {
            InboxItems = new ObservableCollection<InboxItem>();
            SaveInboxItemCommand = new Command(SaveInboxItem, SaveInboxItemCanExecute);
        }

        private bool SaveInboxItemCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private void SaveInboxItem(object obj)
        {
            var result = FirestoreInboxHelper.InsertInboxItem(new Model.InboxItem
            {
                Name = Name,
                Notes = Notes,
                UserId = Auth.GetCurrentUserId(),
                StartDate = DateTime.Today,
                DueDate = StartDate.AddDays(1),
                Id="nlfkjdbnjkfgds"
            });
            if (result.Keys.First())
                App.Current.MainPage.Navigation.PopAsync();
            else
                App.Current.MainPage.DisplayAlert("Error", result.Values.First(), "Ok");
        }

        public async void ReadInboxItems()
        {
            var inboxItems = await FirestoreInboxHelper.ReadInboxItems();

            InboxItems.Clear();
            foreach(var i in inboxItems)
            {
                InboxItems.Add(i);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

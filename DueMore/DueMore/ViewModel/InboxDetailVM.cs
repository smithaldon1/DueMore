using DueMore.DAL;
using DueMore.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DueMore.ViewModel
{
    public class InboxDetailVM : INotifyPropertyChanged
    {
        private InboxItem inboxItem;
        public InboxItem InboxItem
        {
            get
            {
                return inboxItem;
            }
            set
            {
                inboxItem = value;
                Name = inboxItem.Name;
                Notes = inboxItem.Notes;
                StartDate = inboxItem.StartDate;
                DueDate = inboxItem.DueDate;
                IsActive = inboxItem.IsActive;
                OnPropertyChanged("InboxItem");
            }
        }

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
                InboxItem.Name = name;
                OnPropertyChanged("Name");
                OnPropertyChanged("InboxItem");
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
                InboxItem.Notes = notes;
                OnPropertyChanged("Notes");
                OnPropertyChanged("InboxItem");
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
                InboxItem.StartDate = startDate;
                OnPropertyChanged("StartDate");
                OnPropertyChanged("InboxItem");
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
                InboxItem.DueDate = dueDate;
                OnPropertyChanged("DueDate");
                OnPropertyChanged("InboxItem");
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                InboxItem.IsActive = isActive;
                OnPropertyChanged("IsActive");
                OnPropertyChanged("InboxItem");
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InboxDetailVM()
        {
            UpdateCommand = new Command(Update, UpdateCanExecute);
            DeleteCommand = new Command(Delete);
        }

        private bool UpdateCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Name);
        }

        private async void Update(object parameter)
        {
            bool result = await FirestoreInboxHelper.UpdateInboxItem(InboxItem);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "There was an unknown error, please try again", "Ok");
        }

        private async void Delete(object parameter)
        {
            bool result = await FirestoreInboxHelper.DeleteInboxItem(InboxItem);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "There was an unknown error, please try again", "Ok");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

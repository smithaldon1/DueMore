using DueMore.Models;
using DueMore.ViewModels.Helpers;
using DueMore.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DueMore.ViewModels
{

    class InboxDetailVM : INotifyPropertyChanged
    {
        private InboxItems inboxItem;
        public InboxItems InboxItems
        {
            get
            {
                return inboxItem;
            }
            set
            {
                inboxItem = value;
                ItemName = inboxItem.ItemName;
                Notes = inboxItem.Notes;
                OnPropertyChanged("InboxItem");
            }
        }

        private string itemName;
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
                InboxItems.ItemName = itemName;
                OnPropertyChanged("ItemName");
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
                InboxItems.Notes = notes;
                OnPropertyChanged("Notes");
                OnPropertyChanged("InboxItem");
            }

        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InboxDetailVM() //constructor
        {
            UpdateCommand = new Command(Update, UpdateCanExecute);
            DeleteCommand = new Command(Delete);
        }

        private bool UpdateCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(ItemName);
        }

        private async void Update(object parameter)
        {
            bool result = await FirestoreInboxHelper.UpdateInboxItem(InboxItems);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "OK");
        }

        private async void Delete(object parameter)
        {
            bool result = await FirestoreInboxHelper.DeleteInboxItem(InboxItems);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "Something went wrong, please try again", "OK");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}

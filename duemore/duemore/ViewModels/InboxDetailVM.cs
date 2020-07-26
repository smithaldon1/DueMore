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
        private InboxItems inboxItems;
        public InboxItems InboxItems
        {
            get
            {
                return inboxItems;
            }
            set
            {
                inboxItems = value;
                ItemName = inboxItems.ItemName;
                Notes = inboxItems.Notes;
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

        private void Update(object parameter)
        {
            FirestoreInboxHelper.UpdateInboxItem(InboxItems);
        }

        private void Delete(object parameter)
        {
            FirestoreInboxHelper.DeleteInboxItem(InboxItems);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}

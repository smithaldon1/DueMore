using DueMore.Models;
using DueMore.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DueMore.ViewModels
{
    
    public class NewInboxItemVM : INotifyPropertyChanged
    {

        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; OnPropertyChanged("ItemName"); }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; OnPropertyChanged("Notes"); }
            
        }

        public ICommand SaveInboxItemCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public NewInboxItemVM()
        {
            SaveInboxItemCommand = new Command(SaveInboxItem, SaveInboxItemCanExecute);
        }

        private bool SaveInboxItemCanExecute(object arg)
        {
            return !string.IsNullOrEmpty(ItemName);
        }

        private async void SaveInboxItem(object obj)
        {
            bool result = FirestoreInboxHelper.AddInboxItem(new Models.InboxItems
            {
                ItemName = ItemName,
                Notes = Notes,
                UserId = Auth.GetCurrentId()
            });
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

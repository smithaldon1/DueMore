using DueMore.Models;
using DueMore.ViewModels.Helpers;
using DueMore.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DueMore.ViewModels
{
    public class InboxItemsVM : INotifyPropertyChanged
    {
        private InboxItems selectedInboxItem;
        public InboxItems SelectedInboxItems
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
                    App.Current.MainPage.Navigation.PushAsync(new InboxDetailPage(selectedInboxItem));
            }
        }
        public ObservableCollection<InboxItems> InboxItems { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public InboxItemsVM()
        {
            InboxItems = new ObservableCollection<InboxItems>();
        }


        public async void ReadInboxItems()
        {
            var inboxItems = await FirestoreInboxHelper.ReadInbox();

            InboxItems.Clear(); //may need to remove this
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

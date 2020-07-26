using DueMore.ViewModels;
using Xamarin.Forms;
using DueMore.Models;

namespace DueMore.Views
{
    public partial class NewInboxItemPage : ContentPage
    {
        NewInboxItemVM vm;
        public NewInboxItemPage(NewInboxItemVM vm)
        {
            InitializeComponent();

            BindingContext = this.vm = vm;
        }

        public NewInboxItemPage()
        {
            InitializeComponent();

            var inboxitem = new InboxItems
            {
                ItemName = "Enter the item name",
                Notes = "Enter notes for the item."
            };

            vm = new NewInboxItemVM();
            BindingContext = vm;
        }
    }
}
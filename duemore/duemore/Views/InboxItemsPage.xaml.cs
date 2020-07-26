using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using DueMore.Models;
using DueMore.ViewModels;
using DueMore.ViewModels.Helpers;

namespace DueMore.Views
{
    public partial class InboxItemsPage : ContentPage
    {
        InboxItemsVM vm;
        public InboxItemsPage()
        {
            InitializeComponent();
            vm = Resources["vm"] as InboxItemsVM;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!Auth.IsAuthenticated())
            {
                await Task.Delay(300);
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                vm.ReadInboxItems();
            }
        }

        public void Info_Clicked(object sender, EventArgs e)
        {
            
        }

        public async void Detail_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewInboxItemPage());
        }
    }
}
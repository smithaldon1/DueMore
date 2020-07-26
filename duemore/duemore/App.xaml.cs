using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using DueMore.Views;
using DueMore.Models;
using DueMore.ViewModels;
using DueMore.ViewModels.Helpers;

namespace DueMore
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new InboxItemsPage());
        }

        public async void Save_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("Accepted", "Your inbox item has been created", "OK");
        }

        public async void Detail_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewInboxItemPage());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using DueMore.DAL;
using DueMore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DueMore.View
{
    public partial class InboxPageView : ContentPage
    {
        InboxItemsVM vm;
        public InboxPageView()
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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}
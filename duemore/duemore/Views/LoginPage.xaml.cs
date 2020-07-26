using System;
using Xamarin.Forms;

namespace DueMore.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        void RegisterLabel_Tapped(object sender, EventArgs e)
        {
            registerStackLayout.IsVisible = true;
            loginStackLayout.IsVisible = false;
        }

        void LoginLabel_Tapped(object sender, EventArgs e)
        {
            registerStackLayout.IsVisible = false;
            loginStackLayout.IsVisible = true;
        }
    }
}
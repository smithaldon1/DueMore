using DueMore.ViewModels.Helpers;
using System;
using DueMore.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DueMore.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
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
                OnPropertyChanged("Name");
                OnPropertyChanged("CanRegister");
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged("Email");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
                OnPropertyChanged("CanRegister");
            }
        }

        public bool CanLogin
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
        }

        public bool CanRegister
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword) && !string.IsNullOrEmpty(Name);
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand OpenWebCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public LoginVM()
        {
            LoginCommand = new Command(Login, LoginCanExecute);
            RegisterCommand = new Command(Register, RegisterCanExecute);
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://smithbros.dev/privacy-policy"));
        }

        private async void Login(object obj)
        {
            bool result = await Auth.AuthenticateUser(Email, Password);
            if (result == true)
                await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private bool LoginCanExecute(object obj)
        {
            return CanLogin;
        }

        private async void Register(object obj)
        {
            if (ConfirmPassword != Password)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Passwords do not match", "OK");
            }
            else
            {
                bool result = await Auth.RegisterUser(Name, Email, Password);
                if (result)
                    await App.Current.MainPage.Navigation.PopModalAsync();
            }
        }
        private bool RegisterCanExecute(object obj)
        {
            return CanRegister;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


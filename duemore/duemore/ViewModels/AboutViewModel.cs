using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DueMore.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        public AboutViewModel()
        {
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://smithbros.dev"));
        }

        public ICommand OpenWebCommand { get; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
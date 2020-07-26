using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DueMore.ViewModels.Helpers
{
    public interface IAuth
    {
        Task<bool> RegisterUser(string name, string email, string password);
        Task<bool> AuthenticateUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentId();
    }
    public class Auth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();

        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                return await auth.RegisterUser(name, email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public static async Task<bool> AuthenticateUser(string email, string password)
        {
            try
            {
                return await auth.AuthenticateUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return false;
            }
        }

        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }

        public static string GetCurrentId()
        {
            return auth.GetCurrentId();
        }
    }
}

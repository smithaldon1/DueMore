using System;
using System.Threading.Tasks;
using Firebase.Auth;
using DueMore.ViewModels.Helpers;
using Xamarin.Forms;
using Foundation;
using UIKit;

[assembly: Dependency(typeof(DueMore.iOS.Dependencies.Auth))]
namespace DueMore.iOS.Dependencies
{
    public class Auth : IAuth
    {
        public Auth()
        {
        }

        public async Task<bool> AuthenticateUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return true;
            }
            catch (NSErrorException ex)
            {
                string message = ex.Message.Substring(ex.Message.IndexOf("NSLocalizedDescription=", StringComparison.CurrentCulture));
                message = message.Replace("NSLocallizedDescription =", "").Split(".")[0];
                message += ".";

                throw new Exception(message);
            }
            catch (Exception)
            {
                throw new Exception("An unknown error occurred, please try again");
            }
        }

        public string GetCurrentId()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid;
        }

        public bool IsAuthenticated()
        {
            return Firebase.Auth.Auth.DefaultInstance.CurrentUser != null;
        }

        public async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                await Firebase.Auth.Auth.DefaultInstance.CreateUserAsync(email, password);
                //var changeRequest = new Firebase.Auth.

                return true;
            }
            catch (NSErrorException ex)
            {
                string message = ex.Message.Substring(ex.Message.IndexOf("NSLocalizedDescription=", StringComparison.CurrentCulture));
                message = message.Replace("NSLocallizedDescription =", "").Split(".")[0];
                message += ".";

                throw new Exception(message);
            }
            catch (Exception)
            {
                throw new Exception("An unknown error occurred, please try again");
            }
        }
    }
}
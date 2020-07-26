using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using DueMore.Models;
using DueMore.ViewModels;
using DueMore.ViewModels.Helpers;
using Firebase.Firestore;
using Google.Type;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(DueMore.Droid.Dependencies.FirestoreInbox))]
namespace DueMore.Droid.Dependencies
{
    public class FirestoreInbox : Java.Lang.Object, IFirestore, IOnCompleteListener
    {

        List<InboxItems> inboxItems;
        bool hasReadInboxItems = false;

        public FirestoreInbox()
        {
            inboxItems = new List<InboxItems>();
        }

        public bool AddInboxItem(InboxItems inboxItems)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("inboxItems");
                var inboxItemsDocument = new Dictionary<string, Java.Lang.Object>
                {
                    {"author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                    {"item name", inboxItems.ItemName },
                    {"notes", inboxItems.Notes }
                };
                collection.Add(inboxItemsDocument);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Task<bool> DeleteInboxItem(InboxItems inboxItems)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<InboxItems>> ReadInbox()
        {
            hasReadInboxItems = false;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("inboxItems");
            var query = collection.WhereEqualTo("author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
            query.Get().AddOnCompleteListener(this); //Listener acts as an event handler
                
            for(int i = 0; i < 25; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (hasReadInboxItems)
                    break;
            }
            return inboxItems;
        }
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;

                inboxItems.Clear();
                foreach(var doc in documents.Documents)
                {
                    InboxItems inboxItem = new InboxItems
                    {
                        ItemName = doc.Get("item name").ToString(),
                        Notes = doc.Get("notes").ToString(),
                        UserId = doc.Get("author").ToString()
                    };

                    inboxItems.Add(inboxItem);
                }
            }
            else
            {
                inboxItems.Clear();
            }
            hasReadInboxItems = true;
        }

        public Task<bool> UpdateInboxItem(InboxItems inboxItems)
        {
            throw new NotImplementedException();
        }


        //private static Date DateTimeToNativeDate(DateTime date)
        //{
        //long dateTimeUtcAsMilliseconds = (long)date.ToUniversalTime().Subtract(new DateTime(1970,1,1,0,0,0, DateTimeKind.Utc)).TotalMilliseconds;
        //return new Date(dateTimeUtcAsMilliseconds);
        //}

        //private static DateTime NativeDateToDateTime(Java.Util.Date date)
        //{
        //    DateTime reference = System.TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        //    return reference.AddMilliseconds(date.Time);
        //}
    }
}
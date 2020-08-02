using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DueMore.DAL;
using DueMore.Model;
using Firebase.Firestore;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(DueMore.Droid.Dependencies.FirestoreInbox))]
namespace DueMore.Droid.Dependencies
{
    public class FirestoreInbox : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        List<InboxItem> inboxItems;
        bool hasReadInboxItems = false;

        public FirestoreInbox()
        {
            inboxItems = new List<InboxItem>();
        }
        
        public async Task<bool> DeleteInboxItem(InboxItem inboxItem)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("inboxItems");
                collection.Document(inboxItem.Id).Delete();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Dictionary<bool, string> InsertInboxItem(InboxItem inboxItem)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("inboxItems");
                var inboxItemDocument = new Dictionary<string, Java.Lang.Object>
                {
                    { "author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                    { "name", inboxItem.Name },
                    { "notes", inboxItem.Notes },
                    { "startDate", DateTimeToNativeDate(inboxItem.StartDate) },
                    { "dueDate", DateTimeToNativeDate(inboxItem.DueDate) },
                    { "isActive", inboxItem.IsActive }
                };
                collection.Document(inboxItem.Id).Set(inboxItemDocument);
                //collection.Add(inboxItemDocument);

                return new Dictionary<bool, string>() { { true, "Success" } };
            }
            catch(Exception ex)
            {
                return new Dictionary<bool, string>() { { false, ex.Message } };
            }
        }

        public async Task<IList<InboxItem>> ReadInboxItems()
        {
            hasReadInboxItems = false;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("inboxItems");
            var query = collection.WhereEqualTo("author", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
            query.Get().AddOnCompleteListener(this);

            for(int i = 0; i < 25; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (hasReadInboxItems)
                    break;
            }

            return inboxItems;
        }

        public async Task<bool> UpdateInboxItem(InboxItem inboxItem)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("inboxItems");
                collection.Document(inboxItem.Id).Update("name", inboxItem.Name, "notes", inboxItem.Notes, "isActive", inboxItem.IsActive);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private static Date DateTimeToNativeDate(DateTime date)
        {
            long dateTimeUtcAsMilliseconds = (long)date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                ).TotalMilliseconds;
            return new Date(dateTimeUtcAsMilliseconds);
        }

        private static DateTime NativeDateToDateTime(Date date)
        {
            DateTime reference = System.TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddMilliseconds(date.Time);
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;

                inboxItems.Clear();
                foreach(var doc in documents.Documents)
                {
                    InboxItem inboxItem = new InboxItem
                    {
                        IsActive = (bool)doc.Get("isActive"),
                        Name = doc.Get("name").ToString(),
                        Notes = doc.Get("notes").ToString(),
                        UserId = doc.Get("author").ToString(),
                        StartDate = NativeDateToDateTime(doc.Get("startDate") as Date),
                        Id = doc.Id
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
    }
}
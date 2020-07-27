using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DueMore.Models;
using DueMore.ViewModels.Helpers;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DueMore.iOS.Dependencies.FirestoreInbox))]
namespace DueMore.iOS.Dependencies
{
    class FirestoreInbox : IFirestore
    {
        public bool AddInboxItem(InboxItems inboxItems)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("author"),
                    new NSString("item name"),
                    new NSString("notes")
                };

                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(inboxItems.ItemName),
                    new NSString(inboxItems.Notes),
                };

                var inboxItemsDocument = new NSDictionary<NSString, NSObject>(keys, values);
                Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems").AddDocument(inboxItemsDocument);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteInboxItem(InboxItems inboxItem)
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems");
                await collection.GetDocument(inboxItem.Id).DeleteDocumentAsync();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IList<InboxItems>> ReadInbox()
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems");
                var query = collection.WhereEqualsTo("author", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
                var documents = await query.GetDocumentsAsync();

                List<InboxItems> inboxItems = new List<InboxItems>();
                foreach (var doc in documents.Documents)
                {
                    var inboxDictionary = doc.Data;
                    var inboxItem = new InboxItems
                    {
                        ItemName = inboxDictionary.ValueForKey(new NSString("item name")) as NSString,
                        Notes = inboxDictionary.ValueForKey(new NSString("notes")) as NSString,
                        UserId = inboxDictionary.ValueForKey(new NSString("author")) as NSString
                        Id = doc.Id
                    };
                    inboxItems.Add(inboxItem);
                }
                return inboxItems;
            }
            catch(Exception)
            {
                return new List<InboxItems>();
            }
        }

        public async Task<bool> UpdateInboxItem(InboxItems inboxItem)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("item name"),
                    new NSString("notes")
                };

                var values = new NSObject[]
                {
                    new NSString(inboxItem.ItemName),
                    new NSString(inboxItem.Notes),
                };

                var inboxItemsDocument = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems");
                await collection.GetDocument(inboxItem.Id).UpdateDataAsync(inboxItemsDocument);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        //private static NSDate DateTimeToNSDate(DateTime date)
        //{
        //    if (date.Kind == DateTimeKind.Unspecified)
        //        date = DateTime.SpecifyKind(date, DateTimeKind.Local);
        //    return (NSDate)date;
        //}

        //private static DateTime FireTimeToDateTime(Firebase.CloudFirestore.Timestamp date)
        //{
        //    DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
        //    return reference.AddSeconds(date.Seconds);
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DueMore.DAL;
using DueMore.Model;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(DueMore.iOS.Dependencies.FirestoreInbox))]
namespace DueMore.iOS.Dependencies
{
    public class FirestoreInbox : IFirestore
    {
        public FirestoreInbox()
        {
        }

        public async Task<bool> DeleteInboxItem(InboxItem inboxItem)
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems");
                await collection.GetDocument(inboxItem.Id).DeleteDocumentAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Dictionary<bool, string> InsertInboxItem(InboxItem inboxItem)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("author"),
                    new NSString("name"),
                    new NSString("notes"),
                    new NSString("startDate"),
                    new NSString("dueDate"),
                    new NSString("isActive")
                };

                var values = new NSObject[]
                {
                    new NSString(Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid),
                    new NSString(inboxItem.Name),
                    new NSString(inboxItem.Notes),
                    DateTimeToNSDate(inboxItem.StartDate),
                    DateTimeToNSDate(inboxItem.DueDate),
                    new NSNumber(inboxItem.IsActive)
                };

                var inboxItemDocument = new NSDictionary<NSString, NSObject>(keys, values);
                Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems").AddDocument(inboxItemDocument);
                return new Dictionary<bool, string>() { { true, "Success" } };
            }
            catch (Exception ex)
            {
                return new Dictionary<bool, string>() { { false, ex.Message } };
            }
        }

        public async Task<IList<InboxItem>> ReadInboxItems()
        {
            try
            {
                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems");
                var query = collection.WhereEqualsTo("author", Firebase.Auth.Auth.DefaultInstance.CurrentUser.Uid);
                var documents = await query.GetDocumentsAsync();

                List<InboxItem> inboxItems = new List<InboxItem>();
                foreach (var doc in documents.Documents)
                {
                    var inboxItemDictionary = doc.Data;
                    var inboxItem = new InboxItem
                    {
                        IsActive = (bool)(inboxItemDictionary.ValueForKey(new NSString("isActive")) as NSNumber),
                        Name = inboxItemDictionary.ValueForKey(new NSString("name")) as NSString,
                        Notes = inboxItemDictionary.ValueForKey(new NSString("notes")) as NSString,
                        UserId = inboxItemDictionary.ValueForKey(new NSString("author")) as NSString,
                        StartDate = FIRTimeToDateTime(inboxItemDictionary.ValueForKey(new NSString("startDate")) as Firebase.CloudFirestore.Timestamp),
                        DueDate = FIRTimeToDateTime(inboxItemDictionary.ValueForKey(new NSString("dueDate")) as Firebase.CloudFirestore.Timestamp),
                        Id = doc.Id
                    };

                    inboxItems.Add(inboxItem);
                }
                return inboxItems;
            }
            catch (Exception ex)
            {
                return new List<InboxItem>();
            }
        }

        public async Task<bool> UpdateInboxItem(InboxItem inboxItem)
        {
            try
            {
                var keys = new[]
                {
                    new NSString("name"),
                    new NSString("notes"),
                    new NSString("isActive")
                };

                var values = new NSObject[]
                {
                    new NSString(inboxItem.Name),
                    new NSString(inboxItem.Notes),
                    new NSNumber(inboxItem.IsActive)
                };

                var inboxItemDictionary = new NSDictionary<NSObject, NSObject>(keys, values);

                var collection = Firebase.CloudFirestore.Firestore.SharedInstance.GetCollection("inboxItems");
                await collection.GetDocument(inboxItem.Id).UpdateDataAsync(inboxItemDictionary);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static NSDate DateTimeToNSDate(DateTime date)
        {
            if (date.Kind == DateTimeKind.Unspecified)
                date = DateTime.SpecifyKind(date, DateTimeKind.Local);
            return (NSDate)date;
        }

        private static DateTime FIRTimeToDateTime(Firebase.CloudFirestore.Timestamp date)
        {
            DateTime reference = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddSeconds(date.Seconds);
        }
    }
}
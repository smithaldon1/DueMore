using DueMore.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DueMore.DAL
{
    public interface IFirestore
    {
        Dictionary<bool, string> InsertInboxItem(InboxItem inboxItem);
        Task<bool> DeleteInboxItem(InboxItem inboxItem);
        Task<bool> UpdateInboxItem(InboxItem inboxItem);
        Task<IList<InboxItem>> ReadInboxItems();
    }
    public class FirestoreInboxHelper
    {
        private static IFirestore firestore = DependencyService.Get<IFirestore>();

        public static Task<bool> DeleteInboxItem(InboxItem inboxItem)
        {
            return firestore.DeleteInboxItem(inboxItem);
        }

        public static Dictionary<bool, string> InsertInboxItem(InboxItem inboxItem)
        {
            return firestore.InsertInboxItem(inboxItem);
        }

        public static Task<IList<InboxItem>> ReadInboxItems()
        {
            return firestore.ReadInboxItems();
        }

        public static Task<bool> UpdateInboxItem(InboxItem inboxItem) 
        {
            return firestore.UpdateInboxItem(inboxItem);
        }
    }
}

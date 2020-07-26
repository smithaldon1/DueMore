using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DueMore.Models;
using Xamarin.Forms;

namespace DueMore.ViewModels.Helpers
{
    public interface IFirestore
    {
        bool AddInboxItem(InboxItems inboxItems);
        Task<bool> DeleteInboxItem(InboxItems inboxItems);
        Task<bool> UpdateInboxItem(InboxItems inboxItems);
        Task<IList<InboxItems>> ReadInbox();
    }
    public class FirestoreInboxHelper
    {
        private static IFirestore firestore = DependencyService.Get<IFirestore>();

        public static bool AddInboxItem(InboxItems inboxItems)
        {
            return firestore.AddInboxItem(inboxItems);
        }

        public static Task<bool> DeleteInboxItem(InboxItems inboxItems)
        {
            return firestore.DeleteInboxItem(inboxItems);
        }

        public static Task<IList<InboxItems>> ReadInbox()
        {
            return firestore.ReadInbox();
        }

        public static Task<bool> UpdateInboxItem(InboxItems inboxItems)
        {
            return firestore.UpdateInboxItem(inboxItems);
        }
    }
}

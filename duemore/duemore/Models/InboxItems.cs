using System;
using System.Collections.Generic;

namespace DueMore.Models
{
    public class InboxItems
    {
        public string UserId { get; set; }
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Notes { get; set; }
        public IEnumerable<ItemStyle> InboxStyle { get; set; }
    }
}

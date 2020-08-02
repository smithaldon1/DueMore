using System;

namespace DueMore.Model
{
    public class InboxItem
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsActive { get; set; }
    }
}

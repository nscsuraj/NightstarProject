using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class NotificationSearchResult
    {
        [Key]
        public long Id { get; set; }

        public string Header { get; set; }
        public string Detail { get; set; }
        public int Type { get; set; }

        public DateTime CreateDate { get; set; }
        public int UnreadRecords { get; set; }
    }
}

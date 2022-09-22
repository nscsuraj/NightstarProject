using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class NotificationRecipients
    {
        [Key]
        public long Id { get; set; }

        public long NotificationId { get; set; }

        public string Recipient { get; set; }

    }
}

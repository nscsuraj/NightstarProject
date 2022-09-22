using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class NotificationReadBy
    {
        [Key]
        public long Id { get; set; }

        public long NotificationId { get; set; }

        public string ReadBy { get; set; }
        public DateTime ReadDate { get; set; }

    }
}

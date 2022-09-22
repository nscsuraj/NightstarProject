using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class Notifications
    {
        [Key]
        public long Id { get; set; }

        public string Header { get; set; }
        public string Detail { get; set; }
        public int Type { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

    }
}

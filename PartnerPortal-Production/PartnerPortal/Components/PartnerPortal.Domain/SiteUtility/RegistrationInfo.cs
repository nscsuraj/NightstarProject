using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.SiteUtility
{
    public class RegistrationInfo
    {
        [Key]
        public int Id { get; set; }

        public int EventId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string BusinessType { get; set; }
        public string BusinessTypeOther { get; set; }
        public string Company { get; set; }
    }
}

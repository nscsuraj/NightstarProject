using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Accounts
{
    public class PPUsers
    {
        [Key]
        public int Id { get; set; }

        public String AccountId { get; set; }
        public string SessionKey { get; set; }
        public String PartnerNumber { get; set; }
        public string PartnerType { get; set; }
        public bool RememberMe { get; set; }
    }
}

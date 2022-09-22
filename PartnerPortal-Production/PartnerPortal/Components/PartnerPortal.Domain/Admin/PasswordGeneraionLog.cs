using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Admin
{
    public class PasswordGeneraionLog
    {

        [Key]
        public Int64 Id { get; set; }

        public String PartnerNumber { get; set; }


        public DateTime? PasswordGenerationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
    }
}

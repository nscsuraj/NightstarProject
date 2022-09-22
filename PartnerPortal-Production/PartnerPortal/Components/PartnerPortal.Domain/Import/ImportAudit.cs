using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.Import
{
    public class ImportAudit
    {
        [Key]
        public int Id { get; set; }
        public DateTime ImportDate { get; set; }
        public string Comment { get; set; }
        public string ImportStatus { get; set; }
        public string ErrorDetail { get; set; }
        public string ImportSource { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.CMS
{
    public class FileTypeConfig
    {
        [Key]
        public int Id { get; set; }

        public String FileType { get; set; }
        public string Description { get; set; }
        public String Extension { get; set; }
        public string Thumbnail { get; set; }
    }
}

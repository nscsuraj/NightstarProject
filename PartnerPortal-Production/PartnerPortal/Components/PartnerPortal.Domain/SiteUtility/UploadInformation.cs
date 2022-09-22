using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerPortal.Domain.SiteUtility
{
    /// <summary>
    /// Upload Information
    /// </summary>
    public class UploadInformation
    {
       [Key]
       public int Id { get; set; }

        public string OriginalFileName { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string PosterPath { get; set; }

        public DateTime CreateDate { get; set; }

        public string FileSize { get; set; }

        public string FileType { get; set; }

        public double? OriginalWidth { get; set; }

        public double? OriginalHeight { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        public string AltText { get; set; }

        public string Description { get; set; }

        public bool IsAttached { get; set; }

        public int UploadType { get; set; }
        
    }
}

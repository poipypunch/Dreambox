using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDreambox.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        public string PackageID { get; set; }
        [Required]
        [StringLength(100)]
        public string PackageDesc { get; set; }
        public string PackageStatus { get; set; }
        public string CreateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UpdateDate { get; set; }
        //public virtual ICollection<Channel> Channels { get; set; }
        public virtual ICollection<PackageMapping> PackageMappings { get; set; }
    }
}
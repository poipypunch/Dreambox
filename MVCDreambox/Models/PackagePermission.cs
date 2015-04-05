using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDreambox.Models
{
    [Table("PackagePermission")]
    public class PackagePermission
    {
        [Key]
        [Column(Order = 1)] 
        public string PackageID { get; set; }
        [Key]
        [Column(Order = 2)] 
        public string DealerID { get; set; }
        public string CreateBy { get; set; }
        public DateTime ? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime ? UpdateDate { get; set; }
    }
}
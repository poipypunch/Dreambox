using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDreambox.Models
{
    [Table("MemberType")]
    public class MemberType
    {
        [Key]
        public string MemberTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string MemberTypeName { get; set; }
        public string DealerID { get; set; }
        public DateTime ? CreateDate { get; set; }
        public DateTime ? UpdateDate { get; set; }
    }
}
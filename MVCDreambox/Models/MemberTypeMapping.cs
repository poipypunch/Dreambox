using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDreambox.Models
{
    [Table("MemberTypeMapping")]
    public class MemberTypeMapping
    {
        [Key]
        [Column(Order = 1)] 
        public string MemberTypeID { get; set; }
        [Key]
        [Column(Order = 2)] 
        public string CategoryID { get; set; }
        [Key]
        [Column(Order = 3)] 
        public string DealerID { get; set; }
        public string CreateBy { get; set; }
        public DateTime ? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime ? UpdateDate { get; set; }
    }
}
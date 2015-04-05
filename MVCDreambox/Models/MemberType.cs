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
        public MemberType()
        {
            this.member = new HashSet<Member>();
        }
        [Key]
        public string MemberTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string MemberTypeDesc { get; set; }
        public string DealerID { get; set; }
        public string CreateBy { get; set; }
        public DateTime ? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime ? UpdateDate { get; set; }
        public virtual ICollection<Member> member { get; set; }
    }
}
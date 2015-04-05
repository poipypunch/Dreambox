using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace MVCDreambox.Models
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public string MemberID { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(1024)]
        public string Password { get; set; }
        [Required]
        [StringLength(200)]
        public string MemberName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExpiryDate { get; set; }

        [DisplayName("Member type")]
        public string MemberTypeID { get; set; }
        public string DealerID { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public virtual MemberType memberType { get; set; }
        public virtual tbUser tbUser { get; set; }
        //public virtual SysUser sysUser { get; set; }
    }   
}
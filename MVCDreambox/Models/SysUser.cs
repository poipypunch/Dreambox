using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace MVCDreambox.Models
{
    [Table("SysUser")]
    public class SysUser
    {
        [Key]
        public string UserID { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("User name")]
        public string UserName { get; set; }
        [Required]
        [StringLength(1024)]
        public string Password { get; set; }
        [Required]
        [StringLength(200)]
        public string RealName { get; set; }
        [StringLength(20)]
        public string Phone { get; set; }     
       
        //[RegularExpression(@"^\d{10}$")]        
        [StringLength(100)]
        //[RegularExpression(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$")]
        public string Email { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [Required]
        [StringLength(10)]
        public string Status { get; set; }
        [Required]
        [StringLength(10)]
        public string Role { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
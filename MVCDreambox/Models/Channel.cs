using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDreambox.Models
{
    [Table("Channel")]
    public class Channel
    {
        [Key]
        public string ChannelID { get; set; }
        [Required]
        [StringLength(250)]
        public string ChannelDesc { get; set; }
        [StringLength(250)]
        public string ChannelPath { get; set; }
        public string ChannelStatus { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
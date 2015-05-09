using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVCDreambox.Models
{
    [Table("ContentManagement")]
    public class ContentManagement
    {
        [Key]
        [Column(Order = 1)]
        public string CategoryID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ChannelID { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int ChannelOrder { get; set; }
    }
}
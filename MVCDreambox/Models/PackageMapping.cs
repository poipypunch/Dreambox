﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCDreambox.Models
{
    [Table("PackageMapping")]
    public class PackageMapping
    {
        [Key]
        [Column(Order = 1)]
        public string PackageID { get; set; }
        [Key]
        [Column(Order = 2)]
        public string ChannelID { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public virtual Package package { get; set; }
        public virtual Channel channel { get; set; }
    }
}
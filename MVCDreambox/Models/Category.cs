using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDreambox.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public string CategoryID { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }
        [StringLength(250)]
        public string ImgPath { get; set; }
        [Required]
        public string ParentID { get; set; }
        public string DealerID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CategoryDummay
    {
        [Key]
        public string CategoryID { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; }
        [StringLength(250)]
        public string ImgPath { get; set; }
        [Required]
        public string ParentID { get; set; }
        public string DealerID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public HttpPostedFileBase Attachment { get; set; }
    }  
}
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
        public string CategoryDesc { get; set; }
        [StringLength(250)]
        public string ImgPath { get; set; }
        [Required]
        public string ParentID { get; set; }
        public string DealerID { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }


    public class CategoryHierarchy
    {
        public Category Category { get; set; }
        public IEnumerable<CategoryHierarchy> Categorys { get; set; }
    }

    public class HierarchyNode<T> where T : class
    {
        public T Entity { get; set; }
        public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
        public int Depth { get; set; }
    }
}
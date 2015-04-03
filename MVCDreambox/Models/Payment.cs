using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCDreambox.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public string PaymentID { get; set; }
        [Required]
        [StringLength(100)]
        public string PaymentName { get; set; }
        [Required]
        public int PaymentTotalDay { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public decimal PaymentCost { get; set; }       
        public string PaymentStatus { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class PaymentDummy
    {
        [Key]
        public string PaymentID { get; set; }
        [Required]
        [StringLength(100)]
        public string PaymentName { get; set; }
        [Required]
        //[RegularExpression(@"^\d{5}$")]
        public int PaymentTotalDay { get; set; }
        //[RegularExpression(@"^\d{10}$")]
        public decimal PaymentCost { get; set; }
        public DateTime ExpiryDate { get; set; }
        [Required]
        [StringLength(10)]
        public string PaymentStatus { get; set; }
        [Required]
        //[RegularExpression(@"^\d{2}$")]
        public int Quantity { get; set; }
    }
}
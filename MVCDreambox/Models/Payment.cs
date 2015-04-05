using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace MVCDreambox.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        [DisplayName("Package ID")]
        public string PaymentID { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Package name")]
        public string PaymentName { get; set; }
        [Required]
        [DisplayName("Amount of day")]
        public int PaymentTotalDay { get; set; }
        [Required]
        [Column("ExpiryDate")]
        [DisplayName("Expiry date")]
        public DateTime? PaymentExpiryDate { get; set; }
         [DisplayName("Cost")]
        public decimal PaymentCost { get; set; }
        [DisplayName("Status")]
        public string PaymentStatus { get; set; }
        public string CreateBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ? UpdateDate { get; set; }
    }

    public class PaymentDummy
    {
        [Key]
        public string PaymentID { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Package name")]
        public string PaymentName { get; set; }
        [Required]
        //[RegularExpression(@"^\d{5}$")]
        [DisplayName("Amount of day")]
        public int PaymentTotalDay { get; set; }
        //[RegularExpression(@"^\d{10}$")]
        [DisplayName("Cost")]
        public decimal PaymentCost { get; set; }
        [Column("ExpiryDate")]
        [DisplayName("Expiry date")]
        public DateTime? PaymentExpiryDate { get; set; }
        [Required]
        [StringLength(10)]
        [DisplayName("Status")]
        public string PaymentStatus { get; set; }
        [Required]
        //[RegularExpression(@"^\d{2}$")]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
    }
}
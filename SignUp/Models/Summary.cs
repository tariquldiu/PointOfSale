using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    [Table("Summarys")]
    public class Summary
    {
        [Key]
        public int SummaryId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string InvoiceNo { get; set; }
        public string PaymentType { get; set; }
        public int TransDiscount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal AmountTendered { get; set; }
        public decimal Change { get; set; }
        public bool Status { get; set; }
        //[ForeignKey("BankId")]
        public int BankId { get; set; }
        public BankDetail bankDetails { get; set; }
    }
}
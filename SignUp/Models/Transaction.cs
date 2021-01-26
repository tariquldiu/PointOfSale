using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        public string TransVat { get; set; }
        public decimal TransDiscount { get; set; }
        public int TransactionQty { get; set; }
        public decimal SubTotal { get; set; }
        public bool Status { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User users { get; set; }
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer customers { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order orders { get; set; }
    }
}
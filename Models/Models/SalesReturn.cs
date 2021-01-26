using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    [Table("SalesReturns")]
    public class SalesReturn
    {
        [Key]
        public int SalesReturnId { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime PurchesDate { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Reason { get; set; }
        public decimal ReturnAmmount { get; set; }

       // [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

       // [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        //[ForeignKey("TransactionId")]
        //public int TransactionId { get; set; }
        //public Transaction Transaction { get; set; }
        
        //[ForeignKey("StockinId")]
        public int StockinId { get; set; }
        public Stockin Stockin { get; set; }
    }
}
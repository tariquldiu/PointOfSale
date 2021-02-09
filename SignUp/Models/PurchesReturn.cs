using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    [Table("DamagedProducts")]
    public class PurchesReturn
    {
        [Key]
        public int PurchesReturnId { get; set; }
        public DateTime PurchesDate { get; set; }
        public DateTime PurchesReturnDate { get; set; }
        public int Quantity { get; set; }
        public decimal PurchesAmmount { get; set; }
        public string Reason { get; set; }
        public string Size { get; set; }

      //  [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

      //  [ForeignKey("OrdedrId")]
        public int OrderId { get; set; }
        public Order Orders { get; set; }
        
      //  [ForeignKey("StockId")]
        public int StockId { get; set; }
        public Stockin Stockins { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    [Table("DamagedProducts")]
    public class DamagedProduct
    {
        [Key]
        public int DamagedProductId { get; set; }
        public int Quantity { get; set; }
        public string DamagedType { get; set; }

        [ForeignKey("StockinId")]
        public int StockinId { get; set; }
        public Stockin stockins { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
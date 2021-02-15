using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignUp.Models
{
    [Table("BankDetails")]
    public class BankDetail
    {
        [Key]
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountType { get; set; }
        public string AccountFor { get; set; } 
        public int CustomerId { get; set; }  
        public int CompanyId { get; set; }
        public bool Status { get; set; }
        public Company Company { get; set; } 
        public Customer Customer { get; set; } 
    }
}
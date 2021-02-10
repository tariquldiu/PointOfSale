using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignUp.Dto
{
    public class BankDetailListDto
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string BankAccountNo { get; set; }
        public string BankAccountType { get; set; }
        public string PaymentType { get; set; }
        public string BankTransactionNo { get; set; }
        public bool Status { get; set; }
    }
}
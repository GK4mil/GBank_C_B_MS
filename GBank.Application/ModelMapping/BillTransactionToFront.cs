using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.ModelMapping
{
    public class BillTransactionToFront
    {
        public int ID { get; set; }
        public String direction { get; set; }
        public DateTime datetime { get; set; }
        public String amount { get; set; }
        public String transactionid { get; set; }

       
    }
}

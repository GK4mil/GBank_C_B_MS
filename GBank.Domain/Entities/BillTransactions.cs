using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Domain.Entities
{
    public class BillTransactions
    {

        public int ID { get; set; }
        public String direction { get; set; }
        public DateTime datetime { get; set; }
        public String transactionid { get; set; }
        public Bill bill { get; set; }
    }
}

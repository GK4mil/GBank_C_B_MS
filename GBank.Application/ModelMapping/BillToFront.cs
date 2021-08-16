using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GBank.Application.ModelMapping
{
    public class BillToFront

    {
        public int ID { get; set; }
         public Decimal balance { get; set; }

        public String billNumber { get; set; }
    }
}
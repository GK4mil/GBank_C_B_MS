using GBank.Application.ModelMapping;
using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.Transfer.Query
{
    public class GetBillTransactionsQuery
         : IRequest<List<BillTransactionToFront>>
    {
        public String username;
        public String billNumber;

    }
  
}

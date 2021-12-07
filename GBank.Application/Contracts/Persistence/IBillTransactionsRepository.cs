using GBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GBank.Application.Contracts.Persistence
{
    public interface IBillTransactionsRepository : IAsyncRepository<BillTransactions>
    {
        Task<List<BillTransactions>> GetTransactionsByBillId(int billId);

    }
}

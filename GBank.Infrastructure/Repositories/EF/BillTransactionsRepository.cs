using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using GBank.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBank.Infrastructure.Repositories.EF
{
    public class BillTransactionsRepository : BaseRepository<BillTransactions>, IBillTransactionsRepository
    {
        public BillTransactionsRepository(GBankDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<BillTransactions>> GetTransactionsByBillId(int billId)
        {
            return (List<BillTransactions>)await Task.Run(() => (_dbContext.BillTransactions.Where(x => x.bill.ID == billId)).ToList());
        }
    }
}

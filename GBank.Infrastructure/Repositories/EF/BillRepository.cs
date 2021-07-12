using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using GBank.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GBank.Infrastructure.Repositories.EF
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(GBankDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<List<Bill>> GetBillsByUserId(int userId)
        {
            return (List<Bill>)(await _dbContext.Users.FirstAsync(x => x.ID == userId)).Bills;
            //return await _dbContext.Bills.Where(x => x.Users.Where(y=>y.ID == userId)).Include(c=>c.User).ToListAsync();
        }

        public async Task<List<Bill>> FindBybillNumber(String billnr)
        {
            return await _dbContext.Bills.Where(x => x.billNumber == billnr).ToListAsync();
        }
    }
}

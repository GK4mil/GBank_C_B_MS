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
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        private readonly GBankDbContext dbContext;

        public NewsRepository(GBankDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<News>> GetSomeCountOfNews(int count)
        {
           var result = await Task.Run(() => dbContext.News.OrderByDescending(t => t.date).Take(count).ToList());
            return  result;
        }
    }
}

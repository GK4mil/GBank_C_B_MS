using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using GBank.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Infrastructure.Repositories.EF
{
    public class RefreshTokensRepository : BaseRepository<RefreshTokens>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(GBankDbContext dbContext) : base(dbContext)
        {
        }
    }
}

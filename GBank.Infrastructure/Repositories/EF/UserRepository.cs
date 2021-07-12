using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using GBank.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Infrastructure.Repositories.EF
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GBankDbContext dbContext) : base(dbContext)
        {
        }

    }
}

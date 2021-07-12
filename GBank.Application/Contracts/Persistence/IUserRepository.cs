using GBank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
    }
}

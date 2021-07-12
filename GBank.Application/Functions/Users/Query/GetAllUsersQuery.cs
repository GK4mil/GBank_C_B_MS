using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Users.Query
{
    public class GetAllUsersQuery
         : IRequest<List<User>>
    {
        
    }
}

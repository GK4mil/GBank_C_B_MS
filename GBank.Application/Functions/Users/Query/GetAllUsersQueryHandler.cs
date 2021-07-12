using GBank.Application.Common.Interfaces;
using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Users.Query
{
    public class GetAllUsersQueryHandler : IRequestHandler
    <GetAllUsersQuery, List<User>>
    {
        private readonly IUserRepository _ur;

        public GetAllUsersQueryHandler(IUserRepository ur)
        {
            _ur = ur;
        }
        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return (List<User>)await _ur.GetAllAsync();
            
        }
    }
}

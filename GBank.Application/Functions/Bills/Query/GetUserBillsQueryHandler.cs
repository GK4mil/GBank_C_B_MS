using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Bills.Query
{
    public class GetUserBillsQueryHandler : IRequestHandler <GetUserBillsQuery, List<Bill>>
    {
        private readonly IBillRepository _br;

        public GetUserBillsQueryHandler(IBillRepository br)
        {
            _br = br;
        }
        public async Task<List<Bill>> Handle(GetUserBillsQuery request, CancellationToken cancellationToken)
        {
            return await _br.GetBillsByUserId(request.UserId);    
        }
    }
}

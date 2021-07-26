using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.UserBill.Query
{
    public class GetBillsOfUserCommandHandler : IRequestHandler<GetBillsOfUserCommand, List<Bill>>
    {
        private readonly IBillRepository _br;

        public GetBillsOfUserCommandHandler(IBillRepository br)
        {
            _br = br;
            
        }

        public async Task<List<Bill>> Handle(GetBillsOfUserCommand request, CancellationToken cancellationToken)
        {
            return await _br.GetBillsOfUser(request.username);
        }
    }
}

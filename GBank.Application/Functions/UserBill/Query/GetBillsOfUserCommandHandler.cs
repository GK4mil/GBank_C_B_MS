using AutoMapper;
using GBank.Application.Contracts.Persistence;
using GBank.Application.ModelMapping;
using GBank.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.UserBill.Query
{
    public class GetBillsOfUserCommandHandler : IRequestHandler<GetBillsOfUserCommand, List<BillToFront>>
    {
        private readonly IBillRepository _br;
        private IMapper Mapper { get; }

        public GetBillsOfUserCommandHandler(IBillRepository br, IMapper mapper)
        {
            _br = br;
            Mapper = mapper;
        }

        

        public async Task<List<BillToFront>> Handle(GetBillsOfUserCommand request, CancellationToken cancellationToken)
        {
            return Mapper.Map<List<Bill>, List<BillToFront>>(await _br.GetBillsOfUser(request.username));
        }
    }
}

using GBank.Application.Common.Interfaces;
using GBank.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Authentication.Command
{
    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, Tokens>
    {
        private readonly IUserService _us;

        public RefreshCommandHandler(IUserService us)
        {
            _us = us;

        }


        public async Task<Tokens> Handle(RefreshCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(()=>_us.Refresh(request.userClaim, request.refreshToken));

        }
    }
}

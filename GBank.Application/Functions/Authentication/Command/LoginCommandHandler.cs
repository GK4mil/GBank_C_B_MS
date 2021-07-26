using GBank.Application.Common.Interfaces;
using GBank.Application.Common.Models;
using GBankAdminService.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Authentication.Command
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Tokens>
    {
        private readonly IUserService _us;
        private readonly IPasswordHashService _hs;

        public LoginCommandHandler(IUserService us, IPasswordHashService hs)
        {
            _us = us;
            _hs = hs;
        }


        public async Task<Tokens> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => {

                return _us.Login(new GBank.Application.Common.Models.Authentication()
                {
                    username = request.username,
                    password = request.password
                });
            });
            
            
        }
    }
}

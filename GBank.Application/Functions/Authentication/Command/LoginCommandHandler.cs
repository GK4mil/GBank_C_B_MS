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
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Tokens>
    {
        private readonly IUserService _us;

        public LoginCommandHandler(IUserService us)
        {
            _us = us;
            
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

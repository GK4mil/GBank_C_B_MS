using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GBank.Application.Common.Interfaces;
using MediatR;
namespace GBank.Application.Functions.Users.Command
{
    public class AddUserCommandHandler: IRequestHandler<AddUserCommand,int>
    {
        private readonly IUserService _us;

        public AddUserCommandHandler(IUserService us)
        {
            _us = us;
        }
        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {

            return (await Task.Run(()=>_us.Create(new Domain.Entities.User()
            {
                firstname = request.firstname,
                lastname = request.lastname,
                password = request.password,
                Username = request.username
            }))).ID;
        }
    }
}

using GBank.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Authentication.Command
{
    public class LoginCommand:IRequest<Tokens>
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

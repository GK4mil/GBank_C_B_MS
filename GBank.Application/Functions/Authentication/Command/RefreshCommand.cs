using GBank.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Authentication.Command
{
    public class RefreshCommand : IRequest<Tokens>
    {
        public Claim userClaim { get; set; }
        public string refreshToken { get; set; }
    }
}

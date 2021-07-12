using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
namespace GBank.Application.Functions.Users.Command
{
    public class AddUserCommand: IRequest<int>
    {
        
        public String firstname { get; set; }
        public String lastname { get; set; }
        public String username { get; set; }
        public String password { get; set; }

    }
}

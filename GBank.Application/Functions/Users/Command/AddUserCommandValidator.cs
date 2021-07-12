using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace GBank.Application.Functions.Users.Command
{
    class AddUserCommandValidator: AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.firstname).NotEmpty();
            RuleFor(x=>x.lastname).NotEmpty();
            RuleFor(x => x.password).NotEmpty();
            RuleFor(x => x.username).NotEmpty();
        }
    }
}

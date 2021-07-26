using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.UserBill.Query
{
    public class GetBillsOfUserCommand: IRequest<List<Bill>>
    {
       
        public string username { get; set; }
    }
}

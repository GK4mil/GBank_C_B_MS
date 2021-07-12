using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.Bills.Query
{
    public class GetUserBillsQuery
         : IRequest<List<Bill>>
    {
        public int UserId { get; set; }
    }
}

using GBank.Application.ModelMapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.UserBill.Query
{
    public class GetBillDetailsCommand : IRequest<List<BillToFront>>
    {

        public string username { get; set; }
    }
}

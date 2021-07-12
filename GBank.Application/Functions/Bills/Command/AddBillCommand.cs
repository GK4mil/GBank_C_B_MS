using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.Bills.Command
{
    public class AddBillCommand : IRequest<String>
    {
		public String balance { get; set; }
		public int UserID { get; set; }
			
	}
}

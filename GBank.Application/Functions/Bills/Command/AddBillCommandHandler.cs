using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Bills.Command
{
    public class AddBillCommandHandler : IRequestHandler<AddBillCommand, String>
	{
		private readonly IBillRepository _br;
		private readonly IUserRepository _ur;

		public AddBillCommandHandler(IBillRepository br, IUserRepository ur)
		{
			_br = br;
			_ur = ur;
		}
		
		public async Task<String> Handle(AddBillCommand request, CancellationToken cancellationToken)
        {
			var generated_billnumber = await generateBillNumberAsync();
			var findeduserbyid = await _ur.GetByIdAsync(request.UserID);
			Decimal balance_parsed;
			if (Decimal.TryParse(request.balance, out balance_parsed))
			{
				if (findeduserbyid != null)
				{

					return ((Bill)(await _br.AddAsync(new Bill()
					{
						balance = balance_parsed,
						billNumber = generated_billnumber,
						
					}))).billNumber;
				}
				else
				{
					return "Wrong userid";
				}
			}
			else
			{
				return "Wrong balance format";
			}

        }

        private async Task<string> generateBillNumberAsync()
		{
			Random r = new Random();
			String result = "";
			bool flag = false;
			while (!flag)
			{
				while (result.Length < 6)
				{
					result += r.Next()%10;
				}
				if ((await _br.FindBybillNumber(result)).Count == 0)
					flag = true;
				else
					result = "";
			}
			return result;
		}
		
	}
}

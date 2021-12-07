using AutoMapper;
using GBank.Application.Contracts.Persistence;
using GBank.Application.ModelMapping;
using GBank.Domain.Entities;
using MediatR;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GBank.Application.Functions.Transfer.Query
{
    public class GetBillTransactionsQueryHandler : IRequestHandler
    <GetBillTransactionsQuery, List<BillTransactionToFront>>
    {
        private readonly IBillRepository _br;
        private readonly IBillTransactionsRepository _btr;
        private readonly IElasticClient _ec;
        private IMapper _mapper { get; }

        public GetBillTransactionsQueryHandler(IBillRepository br, IBillTransactionsRepository btr, IMapper mapper, IElasticClient ec)
        {
            _br = br;
            _btr = btr;
            _mapper = mapper;
            _ec= ec;
        }
        public async Task<List<BillTransactionToFront>> Handle(GetBillTransactionsQuery request, CancellationToken cancellationToken)
        {
            var billList = await _br.GetBillsOfUser(request.username);
            List<Bill> checkBillByOwner = billList.FindAll(x => x.billNumber == request.billNumber);
            
            


            if (checkBillByOwner.Count > 0)
            {
                List<BillTransactionToFront> bttr = _mapper.Map<List<BillTransactions>, List<BillTransactionToFront>>(await _btr.GetTransactionsByBillId(checkBillByOwner[0].ID));
                
                foreach(var transaction in bttr)
                {
                    var result = await _ec.SearchAsync<BillLog>(s => s.Index("transactions").
                    Query(q => q.Match(m => m.Field(f => f.transactionID).Query(transaction.transactionid))));
                    if(result.Fields.Count>0)
                    {
                        var newest_result = result?.Documents?.ToList<BillLog>().OrderByDescending(x => x.datetime).ToList().First();
                        transaction.amount = newest_result.amount;
                    }
                    else
                    {
                        transaction.amount ="no available at this moment";
                    }
                    
                }

                return bttr;
            }
            
            return new List<BillTransactionToFront>();

        }

    }
    internal class BillLog
    {

        public DateTime datetime { get; set; }
        public String senderBillNumber { get; set; }
        public String recieverBillNumber { get; set; }
        public String amount { get; set; }
        public String status { get; set; }
        public String optionalInfo { get; set; }
        public String transactionID { get; set; }
    }
    
}

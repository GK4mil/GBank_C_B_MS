using GBank.Application.Contracts.Persistence;
using GBank.Domain.Entities;
using MediatR;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GBank.Application.Functions.Transfer.Command
{
    public class MakeTransferCommandHandler : IRequestHandler<MakeTransferCommand, bool>
    {
        private readonly Plain.RabbitMQ.IPublisher publisher;
        private readonly IElasticClient _ec;
        private readonly IBillTransactionsRepository _tr;
        private readonly IBillRepository _br;
        public MakeTransferCommandHandler(Plain.RabbitMQ.IPublisher publisher, IElasticClient ec, IBillTransactionsRepository tr, IBillRepository br)
        {
            this.publisher = publisher;
            _ec = ec;
            _tr = tr;
            _br = br;
        }
        async Task<bool> IRequestHandler<MakeTransferCommand, bool>.Handle(MakeTransferCommand request, CancellationToken cancellationToken)
        {

            /// sprawdzic jak to działa, czy poprawnie
            request.transactionID = Guid.NewGuid().ToString();
           
            Bill resultBill = null;
            resultBill = (await _br.FindByBillNumber(request.senderBillNumber))[0];

            if (resultBill != null)
                await _tr.AddAsync(new BillTransactions() { datetime = DateTime.Now, direction = "out", bill = resultBill, transactionid = request.transactionID });
            
            await Task.Run(() => { publisher.Publish(JsonConvert.SerializeObject(request), "transfer.make", null); });




            await Task.Run(() =>
            {
                _ec.Index<BillLog>(new BillLog()
                {
                    datetime = DateTime.Now,
                    recieverName=request.recieverName,
                    recieverAddress=request.recieverAddress,
                    title=request.title,
                    amount = request.amount,
                    senderBillNumber = request.senderBillNumber,
                    recieverBillNumber = request.recieiverBillNumber,
                    status = "new",
                    optionalInfo="-",
                    transactionID = request.transactionID
                }, x => x.Index("transactions")); ;
            });
            return true;
        }

        private class BillLog
        {
            public DateTime datetime;
            public String recieverName;
            public String recieverAddress;
            public String title;
            public String amount;
            public String senderBillNumber;
            public String recieverBillNumber; 
            public String status;
            public String optionalInfo;
            public String transactionID;
        }
    }
}

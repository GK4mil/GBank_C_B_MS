using GBank.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace GBank.Application.Functions.Transfer.Command
{
    public class MakeTransferCommandHandler : IRequestHandler<MakeTransferCommand, String>
    {
        private readonly Plain.RabbitMQ.IPublisher publisher;
        public MakeTransferCommandHandler(Plain.RabbitMQ.IPublisher publisher)
        {
            this.publisher = publisher;
        }
        async Task<string> IRequestHandler<MakeTransferCommand, string>.Handle(MakeTransferCommand request, CancellationToken cancellationToken)
        {
            await Task.Run(() => { publisher.Publish(JsonConvert.SerializeObject(request), "transfer.make", null); });
            
            return  "Dodano";
        }
    }
}

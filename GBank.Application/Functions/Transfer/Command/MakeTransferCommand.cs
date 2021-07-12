using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.Transfer.Command
{
    
    public class MakeTransferCommand : IRequest<String>
    {
        public String amount { get; set; }
        public String senderBillNumber { get; set; }
        public String recieiverBillNumber { get; set; }

    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBank.Application.Functions.Transfer.Command
{
    
    public class MakeTransferCommand : IRequest<bool>
    {
        public String recieverName { get; set; }
        public String recieverAddress { get; set; } 
        public String title { get; set; } 
        public String amount { get; set; } 
        public String senderBillNumber { get; set; }  
        public String recieiverBillNumber { get; set; } 
        public String transactionID { get; set; }

    }
}

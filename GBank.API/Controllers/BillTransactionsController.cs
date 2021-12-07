using GBank.Application.Functions.Transfer.Query;
using GBank.Application.ModelMapping;
using GBank.Domain.Entities;
using GBank.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillTransactionsController : ControllerBase
    {
        private readonly ILogger<BillTransactionsController> _logger;
        private readonly IMediator _mediator;
        private readonly ITokenService _ts;
        private readonly IElasticClient _ec;

        public BillTransactionsController(ILogger<BillTransactionsController> logger, IMediator mediator, ITokenService ts, IElasticClient ec)
        {
            _logger = logger;
            _mediator = mediator;
            _ts = ts;
            _ec= ec;
        }

        [HttpGet("{billID}")]
         public async Task<List<BillTransactionToFront>> Get(int billID)
         {
            var result =await _mediator.Send(new GetBillTransactionsQuery() { username = await _ts.GetUsernameFromToken(Request.Headers["Authorization"]), billNumber = billID.ToString() });
            result = result?.OrderByDescending(x => x.datetime).ToList();
            return result;
         }

        [HttpGet("details/{id}")]
        public async Task<List<BillLog>> GetDetails(Guid id)
        {
            Console.WriteLine(id);
            var result = await _ec.SearchAsync<BillLog>(s => s.Index("transactions").
            Query(q => q.Match(m => m.Field(f => f.transactionID).Query(id.ToString()))));


            //var resulttest = await _mediator.Send(new GetBillTransactionsQuery() { username = await _ts.GetUsernameFromToken(Request.Headers["Authorization"]), billNumber = id.ToString() });
            
            return result?.Documents?.ToList<BillLog>();

        }
        [HttpGet("newestdetail/{id}")]
        public async Task<BillLog> GetNewestDetail(Guid id)
        {
            var result = await _ec.SearchAsync<BillLog>(s => s.Index("transactions").
            Query(q => q.Match(m => m.Field(f => f.transactionID).Query(id.ToString()))));
            //var resulttest = await _mediator.Send(new GetBillTransactionsQuery() { username = await _ts.GetUsernameFromToken(Request.Headers["Authorization"]), billNumber = id.ToString() });
            var newest_result = result?.Documents?.ToList<BillLog>().OrderByDescending(x=> x.datetime).ToList().First();
            return newest_result;
        }



    }
    public class BillLog
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

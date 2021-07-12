using GBank.Application.Functions.Bills.Query;
using GBank.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;

        private readonly IMediator _mediator;

        public BillController(ILogger<BillController> logger, IMediator mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }
        public async Task<OkResult> Index()
        {
            return Ok();
        }

        [HttpPost("billslist")] 
        public async Task<ActionResult<List<Bill>>> GetBillsByUserId([FromBody] GetUserBillsQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}

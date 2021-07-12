using GBank.Application.Functions.Bills.Command;
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
    public class AdminController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;

        private readonly IMediator _mediator;

        public AdminController(ILogger<BillController> logger, IMediator mediator)
        {
            _logger = logger;

            _mediator = mediator;
        }
        

        [HttpPost("addbill")]
        public async Task<String> GetBillsByUserId([FromBody] AddBillCommand query)
        {
            return await _mediator.Send(query);
        }
    }
}

using GBank.Application.Functions.UserBill.Query;
using GBank.Domain.Entities;
using GBank.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GBank.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly ILogger<BillController> _logger;

        private readonly IMediator _mediator;
        private readonly ITokenService _ts;

        public BillController(ILogger<BillController> logger, IMediator mediator, ITokenService ts)
        {
            _logger = logger;

            _mediator = mediator;
            _ts = ts;
        }
        [HttpGet]
        public async Task<List<Bill>> Get()
        {
            return await _mediator.
                Send( new GetBillsOfUserCommand() { username= await _ts.GetUsernameFromToken(Request.Headers["Authorization"]) } );
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }


    }
}

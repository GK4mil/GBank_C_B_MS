

using GBank.Application.Common.Interfaces;
using GBank.Application.Functions.Transfer.Command;
using GBank.Application.Functions.Users.Command;
using GBank.Application.Functions.Users.Query;
using GBank.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IMediator _mediator;
       

        public UserController(ILogger<UserController> logger, IUserService userService, IMediator mediator)
        {
            _logger = logger;
            
            _mediator = mediator;
        }
        public async Task<OkResult> Index()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<User>>> Get()
        {          
            return await _mediator.Send(new GetAllUsersQuery());
        }

      /*  [HttpGet("{userId}", Name = "GetOne")]
        public User GetOne([FromRoute] string userId)
        {
            return _userService.Get(Int32.Parse(userId));
        }
      */
        [HttpPost("create")]
        public async Task<ActionResult<int>> CreateUser ([FromBody] AddUserCommand command)
        {
            if (ModelState.IsValid)
                return await _mediator.Send(command);
            else
                return BadRequest(ModelState);
        }

        [HttpPost("maketransfer")]
        public async Task<ActionResult<String>> MakeTransfer([FromBody] MakeTransferCommand command)
        {
                return await _mediator.Send(command);         
        }

        /* [HttpPut("{userId}")]
         public User UpdateOne([FromRoute] string userId, [FromBody] User user)
         {
             _userService.Update(userId, user);
             return user;
         }

         [HttpDelete("{userId}")]
         public IActionResult DeleteOne([FromRoute] string userId)
         {

             _userService.Remove(userId);
             return NoContent();
         }
        */
    }
}

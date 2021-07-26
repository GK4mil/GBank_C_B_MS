using GBank.Application.Common.Interfaces;
using GBank.Application.Common.Models;
using GBank.Application.Functions.Authentication.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public TokenController(ILogger<TokenController> logger, IUserService userService, IMediator mediator)
        {
            _logger = logger;
            _userService = userService;
            _mediator = mediator;
        }



        [HttpPost("accesstoken", Name = "login")]
        public async Task<ActionResult<String>> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            
            if (result == null)
                return Unauthorized("Wrong credentials");
            return Ok(result);
            
        }

        [Authorize(AuthenticationSchemes = "refresh")]
        [HttpPut("accesstoken", Name = "refresh")]
        public IActionResult Refresh()
        {
            //Claim refreshtoken = User.Claims.FirstOrDefault(x => x.Type == "refresh");
            Claim username = User.Claims.FirstOrDefault(x => x.Type == "username");

            Request.Headers.TryGetValue("Authorization", out var refreshTockenValue);
            try
            {

                //przekazac string zamiast claim
                return Ok(_userService.Refresh(username, refreshTockenValue));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

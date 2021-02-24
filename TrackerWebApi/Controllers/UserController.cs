using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using TrackerWebApi.Exceptions;
using TrackerWebApi.Model;
using TrackerWebApi.Services;

namespace TrackerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            try 
            { 
                var response = _userService.Authenticate(request);
                return Ok(response);
            }
            catch (AuthenticationException)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationRequest request)
        {
            try 
            { 
                var response = _userService.Register(request);
                return Ok(response);
            }
            catch (RegistrationException)
            {
                return BadRequest(new { message = "User with this name already exists." });
            }
        }
    }
}

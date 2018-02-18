using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Services.Core.Users;

namespace WebPortal.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        protected static IUsersService UsersService { get; private set; }

        public UsersController(IUsersService usersService)
        {
            UsersService = usersService;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUpUser([FromBody]UserAccount userAccount)
        {
            try
            {
                UserAccount user = UsersService.SignUp(userAccount);
                if (user != null)
                {
                    return Created(Request.Path + userAccount.Id, userAccount);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]UserAccount userAccount)
        {
            try
            {
                UserAccount user = UsersService.SignIn(userAccount.Username, userAccount.Password);
                if (user != null)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
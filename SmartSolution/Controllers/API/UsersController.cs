using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebPortal.Models;
using WebPortal.Services.Core.Users;

namespace WebPortal.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        protected static IUsersService UsersService{get; private set;}

        public UsersController(IUsersService usersService)
        {
            UsersService = usersService;
        }

        [HttpPost("{id}")]
        public IActionResult Post(string id, [FromBody]UserAccount user)
        {
            try
            {
                bool result = UsersService.SignUp(user);
                if (result == true)
                {
                    return Created(Request.Path + user.Id, user);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex);
            }
        }

        [HttpPost("Verify")]
        public IActionResult Verify([FromBody]UserAccount userAccount) {
            try
            {
                bool verification = UsersService.SignIn(userAccount.Username, userAccount.Password);
                if (verification == true)
                {
                    return StatusCode(200);
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
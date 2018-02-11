using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartSolutionAPILib;
using WebPortal.Models;
using WebPortal.Services.Core.Users;

namespace WebPortal.Controllers
{
    public class UsersController : Controller
    {
        protected static IUsersService UsersService { get; private set; }

        public UsersController(IUsersService usersService)
        {
            UsersService = usersService;
        }


        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        /// <summary>
        /// Show sing in page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignUp(UserAccount user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                IRestResponse response = SmartSolutionAPI.Post(UsersService.configuration.APIBaseAddress, UsersService.configuration.Users, user.Id, user);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //User is created
                    return RedirectToAction("SignIn", "Home", null);
                }
                else
                {
                    //Bad request, here we need to show message "user already exist"
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home", null);
            }
        }
        /// <summary>
        /// Show login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SignIn(UserAccount user)
        {
            return View();
        }
    }
}
    

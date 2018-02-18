using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using SmartSolutionAPILib;
using WebPortal.Models;
using WebPortal.Services.Core.Users;

namespace WebPortal.Controllers
{

    public class AccountController : Controller
    {
        protected static IUsersService UsersService { get; private set; }

        public AccountController(IUsersService usersService)
        {
            UsersService = usersService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        /// <summary>
        /// Sing up page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        /// <summary>
        /// Sign up
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignUp(UserAccount user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                IRestResponse response = SmartSolutionAPI.Post(UsersService.Configuration.APIBaseAddress, UsersService.Configuration.Users + "/SignUp", "", user);
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    //User is created
                    return RedirectToAction("SignIn", "Account", null);
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
        /// Sign in page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignIn(UserAccount userAccount)
        {
            try
            {
                IRestResponse restResponse = SmartSolutionAPI.Post(UsersService.Configuration.APIBaseAddress, UsersService.Configuration.Users + "/SignIn", "", userAccount);
                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index", "Home", null);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Error");
            }
        }
    }
}


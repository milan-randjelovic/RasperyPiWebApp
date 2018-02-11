﻿using WebPortal.Models;

namespace WebPortal.Services.Core.Users
{
    public interface IUsersService
    {
        /// <summary>
        /// Add new user to database
        /// </summary>
        /// <param name="user"></param>
        bool SignUp(User user);
        /// <summary>
        /// Compare user info with db's info
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        void SignIn(string username, string password);
        /// <summary>
        /// Configuration for database
        /// </summary>
        ApplicationConfiguration configuration { get; set; }
    }
}

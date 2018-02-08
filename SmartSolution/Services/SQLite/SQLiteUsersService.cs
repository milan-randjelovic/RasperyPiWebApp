using System;
using WebPortal.Models;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Users;
using WebPortal.Services.SQLite;

namespace WebPortal.Services.Mongo
{
    public class SQLiteUsersService : UserService
    {
        private SQLiteDbContext dbContext;

        public SQLiteUsersService(IDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            try
            {
                this.dbContext = (SQLiteDbContext)dbContext;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void SignUp(User user)
        {
            try
            {
                //Check if its valid (already exist) in users and usersAppending
                // If it is valid
                //add user to users appending and get back to login
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void LogIn(string username, string password)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }      
    }
}

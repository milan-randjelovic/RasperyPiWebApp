using System;
using WebPortal.Models;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Users;
using WebPortal.Services.SQLite;

namespace WebPortal.Services.Mongo
{
    public class SQLiteUsersService : UsersService
    {
        private SQLiteDbContext dbContext;

        public SQLiteUsersService(ISQLiteDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
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

        /// <summary>
        /// Save changes od db context
        /// </summary>
        private void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public override bool SignIn(User user)
        {
            try
            {
                //Check if its valid (already exist) in users and usersAppending
                // If it is valid
                //add user to users appending and get back to login
                return false;
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

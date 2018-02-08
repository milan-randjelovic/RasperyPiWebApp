using System;
using WebPortal.Models;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Users;

namespace WebPortal.Services.Mongo
{
    public class MongoUsersService : UserService
    {
        MongoDbContext dbContext;

        public MongoUsersService(IDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            try
            {
                this.dbContext = (MongoDbContext)dbContext;
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

using MongoDB.Driver;
using System;
using WebPortal.Models;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Users;

namespace WebPortal.Services.Mongo
{
    public class MongoUserService : UsersService
    {
        MongoDbContext dbContext;

        public MongoUserService(IMongoDbContext DbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            try
            {
                this.dbContext = (MongoDbContext)DbContext;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool SignUp(User user)
        {
            try
            {
                //Check if its valid (already exist) in users and usersAppending
                User userExist  = this.dbContext.Users.Find(u => u.Username == user.Username || u.Email == user.Email).SingleOrDefault();
                if (userExist == null)
                {
                    user.Status = UserStatus.UserAppending;
                    this.dbContext.Users.InsertOne(user);
                    return true;
                }
                else {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void SignIn(string username, string password)
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

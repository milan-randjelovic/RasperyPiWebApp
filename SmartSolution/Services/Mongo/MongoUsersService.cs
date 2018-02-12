using MongoDB.Driver;
using System;
using WebPortal.Models;
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

        /// <summary>
        /// Sign up user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override UserAccount SignUp(UserAccount userAccount)
        {
            try
            {
                UserAccount user  = this.dbContext
                    .Users
                    .Find
                    (
                        u => u.Username == userAccount.Username ||
                        u.Email == userAccount.Email
                    )
                    .SingleOrDefault();

                if (user == null)
                {
                    userAccount.Status = UserStatus.PengingRegistration;
                    this.dbContext.Users.InsertOne(userAccount);
                    return userAccount;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sign in user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public override UserAccount SignIn(string username, string password)
        {
            try
            {
                UserAccount userAccount = this.dbContext
                    .Users
                    .Find
                    (
                        u => u.Username == username &&
                        u.Password == password
                    )
                    .FirstOrDefault();

                return userAccount;
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }      
    }
}

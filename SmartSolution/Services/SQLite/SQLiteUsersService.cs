using System;
using System.Linq;
using WebPortal.Models;
using WebPortal.Services.Core;
using WebPortal.Services.Core.Users;
using WebPortal.Services.SQLite;

namespace WebPortal.Services.Mongo
{
    public class SQLiteUsersService : UsersService
    {

        public SQLiteUsersService(ISQLiteDbContext dbContext, ApplicationConfiguration configuration) : base(configuration)
        {
            try
            {
                using (SQLiteDbContext context = new SQLiteDbContext(this.Configuration))
                {
                    context.Database.EnsureCreated();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sign up user
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public override UserAccount SignUp(UserAccount userAccount)
        {
            try
            {
                using (SQLiteDbContext dbcontext = new SQLiteDbContext(this.Configuration))
                {
                    UserAccount user = dbcontext
                        .Users
                        .Where
                        (
                            u => u.Username == userAccount.Username &&
                            u.Password == userAccount.Password
                        )
                        .FirstOrDefault();

                    if (user == null)
                    {
                        userAccount.Status = UserStatus.PengingRegistration;
                        dbcontext.Add(userAccount);
                        dbcontext.SaveChanges();
                        return userAccount;
                    }
                    else
                    {
                        return null;
                    }
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
                using (SQLiteDbContext dbcontext = new SQLiteDbContext(this.Configuration))
                {
                    UserAccount userAccount = dbcontext
                        .Users
                        .Where
                        (
                            u => u.Username == username &&
                            u.Password == password
                        )
                        .FirstOrDefault();

                    return userAccount;
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }      
    }
}

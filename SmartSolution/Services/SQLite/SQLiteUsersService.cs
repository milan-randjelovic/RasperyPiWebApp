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
                using (SQLiteDbContext context = new SQLiteDbContext(this.configuration))
                {
                    context.Database.EnsureCreated();
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool SignUp(UserAccount user)
        {
            try
            {
                using (SQLiteDbContext dbcontext = new SQLiteDbContext(this.configuration))
                {
                    UserAccount userAccount = dbcontext.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                    if (userAccount == null)
                    {
                        dbcontext.Add(user);
                        dbcontext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override bool SignIn(string username, string password)
        {
            try
            {
                using (SQLiteDbContext dbcontext = new SQLiteDbContext(this.configuration))
                {
                    UserAccount userAccount = dbcontext.Users.Where(u => u.Username == username && u.Password == password).FirstOrDefault();
                    if (userAccount == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }      
    }
}

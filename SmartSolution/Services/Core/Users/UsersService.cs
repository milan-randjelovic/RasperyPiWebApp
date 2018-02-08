using WebPortal.Models;

namespace WebPortal.Services.Core.Users
{
    public abstract class UsersService : IUsersService
    {
        public ApplicationConfiguration configuration { get; set; }

        public UsersService(ApplicationConfiguration config)
        {
            this.configuration = config;
        }
        public abstract void SignUp(User user);    
        public abstract void LogIn(string username, string password);         
    }
}

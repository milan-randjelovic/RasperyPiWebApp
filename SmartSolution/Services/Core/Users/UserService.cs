using WebPortal.Models;

namespace WebPortal.Services.Core.Users
{
    public abstract class UserService : IUserService
    {
        public ApplicationConfiguration configuration { get; set; }

        public UserService(ApplicationConfiguration config)
        {
            this.configuration = config;
        }

        public abstract void SignUp(User user);    
        public abstract void LogIn(string username, string password);         
    }
}

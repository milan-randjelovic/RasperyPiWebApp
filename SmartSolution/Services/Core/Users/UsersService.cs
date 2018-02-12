using WebPortal.Models;

namespace WebPortal.Services.Core.Users
{
    public abstract class UsersService : IUsersService
    {
        public ApplicationConfiguration Configuration { get; set; }

        public UsersService(ApplicationConfiguration config)
        {
            this.Configuration = config;
        }
        public abstract UserAccount SignUp(UserAccount user);    
        public abstract UserAccount SignIn(string username, string password);         
    }
}

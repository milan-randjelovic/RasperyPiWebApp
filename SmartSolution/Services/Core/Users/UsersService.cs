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
        public abstract bool SignUp(UserAccount user);    
        public abstract bool SignIn(string username, string password);         
    }
}

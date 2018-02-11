using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortal.Models
{
    public class UserAccount
    {
        
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string Id { get; set; }

        [DataType(DataType.Text)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Lastname { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false)]
        public string Username { get; set; }

       
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        public UserStatus Status { get; set; }

        public UserAccount()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Name = "";
            this.Lastname = "";
            this.Email = "";
            this.Username = "";
            this.Password = "";
            this.Status = UserStatus.User;
        }
    }
    public enum UserStatus { Admin, User, UserAppending}
}

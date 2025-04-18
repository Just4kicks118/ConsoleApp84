using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp84.Models
{
    // This class is basically representing user login detail by saving there username and password
    public class UserAccount
    {

        public string Password { get; set; }
        public string Username { get; set; }

        // This method Initializes a new instance of a user account with default values 
        public UserAccount() { }

        // This method Initializes a user account with inputted username and pass
        public UserAccount(string username, string password)
        {

            Password = password;
            Username = username;
        }

    }
}

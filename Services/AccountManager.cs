using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp84.Models;
using ConsoleApp84.Data;

namespace ConsoleApp84.Services
{
    // This class is basically for the user account creation and retrieval for authentication purposes
    public class AccountManager
    {
        private UserRepository userRepo;

        // This method basically initializes the class 
        public AccountManager()
        {
            userRepo = new UserRepository();
        }

        // This method gets a user account from the repository based on username 
        public UserAccount GetUserByUsername(string username)
        {
            return userRepo.GetUserByUsername(username);
        }

        // This method basically put the newly created user account in the database through the repository
        public void CreateUser(UserAccount account)
        {
            userRepo.AddUser(account);
        }
    }
}

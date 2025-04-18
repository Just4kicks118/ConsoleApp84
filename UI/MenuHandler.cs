using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ConsoleApp84.UI;
using ConsoleApp84.Models;
using ConsoleApp84.Services;

namespace ConsoleApp84.UI
{
    // This class is basically controlls how the login page and main menu page look like also then managing a user instructions

    public class MenuHandler
    {
        private Func<bool> getIsLoggedIn;
        private Action<bool> setIsLoggedIn;
        private AccountManager ManagingUser;
        private ProfileManager ManagingProfile;

        private Action<string> setLoggedInUsername;
        private Func<string> getLoggedInUsername;

        // This method basically Initializes the class with the needed dependencies and callbacks
        public MenuHandler(
            Func<bool> getIsLoggedIn,
            Action<bool> setIsLoggedIn,
            AccountManager accountManager,
            ProfileManager profileManager,
            Action<string> setLoggedInUsername,
            Func<string> getLoggedInUsername)
        {
            this.getIsLoggedIn = getIsLoggedIn;
            this.setIsLoggedIn = setIsLoggedIn;
            this.ManagingUser = accountManager;
            this.ManagingProfile = profileManager;
            this.setLoggedInUsername = setLoggedInUsername;
            this.getLoggedInUsername = getLoggedInUsername;
        }

        // This method basically displays the login/signup menu and handles user choices
        public void ShowLoginMenu()
        {
            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle("\u2764\uFE0F  Dating Managment System \u2764\uFE0F");
            ConsoleUI.PutTextInMiddle("1. \uD83D\uDD11 Login \uD83D\uDD11  ");
            ConsoleUI.PutTextInMiddle("2. \uD83D\uDCDD Sign Up \uD83D\uDCDD");
            ConsoleUI.PutTextInMiddle("3. \u274C Exit \u274C");

            string choice = ConsoleUI.PutUserInputCenter("Write a number : \uD83D\uDD8A");
            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    SignUp();
                    break;
                case "3":
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle("\uD83D\uDC4B Program has terminated. See Ya! \uD83D\uDC4B");
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle("\u274C Not correct input.\u274C");
                    ConsoleUI.ProgramHold();
                    break;
            }
        }

        //This method checks to see if inputted login info against stored login info and updates login state
        private void Login()
        {
            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle("\uD83D\uDD11 Login: \uD83D\uDD11");
            string username = ConsoleUI.PutUserInputCenter("Enter username: \uD83D\uDD8A");
            string password = ConsoleUI.PutUserInputCenter("Enter password: \uD83D\uDD8A");

            UserAccount account = ManagingUser.GetUserByUsername(username);
            if (account != null && account.Password == password)
            {
                setIsLoggedIn(true);
                setLoggedInUsername(username);
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle("\u2705 Logged in successfully! \u2705");
                ConsoleUI.ProgramHold();
            }
            else
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \u274C Not correct login details! \u274C");
                ConsoleUI.ProgramHold();
            }
        }

        // This method creates a new user account
        private void SignUp()
        {
            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle("\uD83D\uDCDD Sign Up: \uD83D\uDCDD");
            string username = ConsoleUI.PutUserInputCenter("Enter new username: \uD83D\uDD8A");
            string password = ConsoleUI.PutUserInputCenter(" Enter new password: \uD83D\uDD8A");

            if (ManagingUser.GetUserByUsername(username) != null)
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle("\u274C Username already exists. \u274C");
                ConsoleUI.ProgramHold();
                return;
            }
            ManagingUser.CreateUser(new UserAccount(username, password));
            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle("\u2705 Account created successfully! \u2705");
            ConsoleUI.ProgramHold();
        }

        // This method basically displays the main menu for logged in users 
        public void ShowMainMenu()
        {
            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle("\u2764\uFE0F Main Menu: \u2764\uFE0F");
            ConsoleUI.PutTextInMiddle("1. \uD83D\uDCDD Create Profile \uD83D\uDCDD");
            ConsoleUI.PutTextInMiddle("2. \uD83D\uDCDD Modify Profile \uD83D\uDCDD");
            ConsoleUI.PutTextInMiddle("3. \uD83D\uDCDD Display Profiles \uD83D\uDCDD");
            ConsoleUI.PutTextInMiddle("4. \uD83D\uDCDD Delete Profile \uD83D\uDCDD");
            ConsoleUI.PutTextInMiddle("5. \uD83D\uDD0D Search Profiles \uD83D\uDD0D");
            ConsoleUI.PutTextInMiddle("6. \uD83D\uDC4B Logout \uD83D\uDC4B");
            ConsoleUI.PutTextInMiddle("7. \uD83D\uDC4B Exit \uD83D\uDC4B");

            string choice = ConsoleUI.PutUserInputCenter("Write a number : \uD83D\uDD8A");
            switch (choice)
            {
                case "1":
                    ManagingProfile.CreateProfile(getLoggedInUsername());
                    break;
                case "2":
                    ManagingProfile.ModifyProfile(getLoggedInUsername());
                    break;
                case "3":
                    ManagingProfile.DisplayProfiles(getLoggedInUsername());
                    break;
                case "4":
                    ManagingProfile.DeleteProfile(getLoggedInUsername());
                    break;
                case "5":
                    ManagingProfile.SearchProfiles(getLoggedInUsername());
                    break;
                case "6":
                    setIsLoggedIn(false);
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle("\u2705 Logged out successfully! \u2705");
                    ConsoleUI.ProgramHold();
                    break;
                case "7":
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle("\uD83D\uDC4B Program has terminated. See Ya! \uD83D\uDC4B");
                    Environment.Exit(0);
                    break;
                default:
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle("\u274C Not correct input. \u274C");
                    ConsoleUI.ProgramHold();
                    break;
            }
        }
    }
}

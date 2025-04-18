using System;
using ConsoleApp84.Services;
using ConsoleApp84.UI;

namespace ConsoleApp84
{
    //This class is basically the entry point to the program  and control the program
    class Program
    {
        // Global flags and variables to track login state and the current user
        static bool isLoggedIn = false;
        static string loggedInUsername = "";
        static MenuHandler menuHandler;
        static ProfileManager profileManager;
        static AccountManager accountManager;

        // This method entry point to program
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            accountManager = new AccountManager();
            profileManager = new ProfileManager();
            menuHandler = new MenuHandler(
                () => isLoggedIn,
                (val) => isLoggedIn = val,
                accountManager,
                profileManager,
                (username) => loggedInUsername = username,
                () => loggedInUsername
            );


            while (true)
            {
                switch (isLoggedIn)
                {
                    case true:
                        menuHandler.ShowMainMenu();
                        break;
                    default:
                        menuHandler.ShowLoginMenu();
                        break;
                }
            }

        }
    }
}

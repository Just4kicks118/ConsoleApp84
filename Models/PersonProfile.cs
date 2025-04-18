using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp84.UI;


namespace ConsoleApp84.Models
{
    // This class is basically holding a users profiles information also helper methods for validating users input 
    public class PersonProfile
    {

        public string MatchPreferences { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Interests { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        // This method creates a profile with default settings
        public PersonProfile() { }

        // This method creates a profile with inputted details 
        public PersonProfile(int id, string name, int age, string gender, string interests, string matchPreferences, string city, string country)
        {
            Country = country;
            City = city;
            MatchPreferences = matchPreferences;
            Interests = interests;
            Gender = gender;
            Id = id;
            Name = name;
            Age = age;
        }

        // This method is for asking user for numeral input until valid info is provided
        public static int AskForInt(string message)
        {
            while (true)
            {

                string input = ConsoleUI.PutUserInputCenter(message);
                if (input?.ToLower() == "exit")
                {
                    return -1;
                }
                if (int.TryParse(input, out int result))
                    return result;
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \u274C Incorrect input. Please enter a valid number or type 'exit'. \u274C");
                ConsoleUI.ProgramHold();
            }
        }

        // This method is for asking user for text input until valid info is provided        
        public static string AskForString(string message)
        {

            string input = ConsoleUI.PutUserInputCenter(message);
            if (input?.ToLower() == "exit")
            {
                Environment.Exit(0);
            }
            return input;
        }

        // This method displays all profile info to program console
        public void ShowProfileDetails()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Interests: {Interests}");
            Console.WriteLine($"Match Preferences: {MatchPreferences}");
            Console.WriteLine($"City: {City}");
            Console.WriteLine($"Country: {Country}");
        }

        // This method checks if the inputted info is larger than allowed amount if so then shrinks it to the max length and displays message.
        public static string CheckingStringLength(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            if (input.Length > maxLength)
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle($" \u274C Input exceeds maximum length of {maxLength} characters. \u274C");
                ConsoleUI.PutTextInMiddle($"Your input will be shrunk to fit.");
                ConsoleUI.ProgramHold();
                return input.Substring(0, maxLength);
            }

            return input;
        }

    }
}

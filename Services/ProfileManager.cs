using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp84.UI;
using ConsoleApp84.Data;
using System.Text.RegularExpressions;


namespace ConsoleApp84.Services
{
    // This class is basically for  creating modifying displaying deleting & searching a user profile.

    public class ProfileManager
    {
        private ProfileRepository profileRepo;

        // This method basically creates a new class and connects it to the  
        public ProfileManager()
        {
            profileRepo = new ProfileRepository();
        }

        //This method basically a user to input picked profile fields and save profile with inputted fields to the database
        public void CreateProfile(string username)
        {
            ConsoleUI.FreshPage();



            Models.PersonProfile profile = new Models.PersonProfile();

            bool continueEditing = true;
            while (continueEditing)
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \uD83C\uDF10 Pick a profile detail you would like to add \uD83C\uDF10");
                ConsoleUI.PutTextInMiddle("1. ID" + (profile.Id > 0 ? $" (Current: {profile.Id})" : ""));
                ConsoleUI.PutTextInMiddle("2. Name" + (!string.IsNullOrEmpty(profile.Name) ? $" (Current: {profile.Name})" : ""));
                ConsoleUI.PutTextInMiddle("3. Age" + (profile.Age > 0 ? $" (Current: {profile.Age})" : ""));
                ConsoleUI.PutTextInMiddle("4. Gender" + (!string.IsNullOrEmpty(profile.Gender) ? $" (Current: {profile.Gender})" : ""));
                ConsoleUI.PutTextInMiddle("5. Interests" + (!string.IsNullOrEmpty(profile.Interests) ? $" (Current: {profile.Interests})" : ""));
                ConsoleUI.PutTextInMiddle("6. Match Preferences" + (!string.IsNullOrEmpty(profile.MatchPreferences) ? $" (Current: {profile.MatchPreferences})" : ""));
                ConsoleUI.PutTextInMiddle("7. City" + (!string.IsNullOrEmpty(profile.City) ? $" (Current: {profile.City})" : ""));
                ConsoleUI.PutTextInMiddle("8. Country" + (!string.IsNullOrEmpty(profile.Country) ? $" (Current: {profile.Country})" : ""));
                ConsoleUI.PutTextInMiddle("9. Save Profile and Return to Main Menu");

                string choice = ConsoleUI.PutUserInputCenter(" \uD83D\uDCF1 Pick an option: \uD83D\uDCF1");

                switch (choice)
                {
                    case "1":
                        int idInput = Models.PersonProfile.AskForInt(" \uD83D\uDCF1 Enter ID: \uD83D\uDCF1");
                        if (idInput == -1) continue;
                        profile.Id = idInput;
                        break;
                    case "2":
                        profile.Name = Models.PersonProfile.AskForString(" \uD83D\uDCF1 Enter Name: \uD83D\uDCF1");
                        break;
                    case "3":
                        int ageInput = Models.PersonProfile.AskForInt(" \uD83D\uDCF1Enter Age: \uD83D\uDCF1");
                        if (ageInput == -1) continue;
                        profile.Age = ageInput;
                        break;
                    case "4":
                        profile.Gender = Models.PersonProfile.AskForString(" \uD83D\uDCF1 Enter Gender: \uD83D\uDCF1");
                        break;
                    case "5":
                        profile.Interests = Models.PersonProfile.AskForString(" \uD83D\uDCF1 Enter Interests: \uD83D\uDCF1");
                        break;
                    case "6":
                        profile.MatchPreferences = Models.PersonProfile.AskForString(" \uD83D\uDCF1 Enter Match Preferences: \uD83D\uDCF1");
                        break;
                    case "7":
                        profile.City = Models.PersonProfile.AskForString(" \uD83D\uDCF1 Enter City: \uD83D\uDCF1");
                        break;
                    case "8":
                        profile.Country = Models.PersonProfile.AskForString(" \uD83D\uDCF1 Enter Country: \uD83D\uDCF1");
                        break;
                    case "9":
                        continueEditing = false;
                        break;
                    default:
                        ConsoleUI.FreshPage();
                        ConsoleUI.PutTextInMiddle(" \u274C Incorrect input. \u274C");
                        ConsoleUI.ProgramHold();
                        break;
                }
            }


            if (profile.Id <= 0)
            {
                Random rnd = new Random();
                profile.Id = rnd.Next(1000, 9999);
            }


            if (string.IsNullOrEmpty(profile.Name))
            {
                profile.Name = "Anonymous User";
            }
            profile.Name = Models.PersonProfile.CheckingStringLength(profile.Name, 100);
            profile.Gender = Models.PersonProfile.CheckingStringLength(profile.Gender, 20);
            profile.Interests = Models.PersonProfile.CheckingStringLength(profile.Interests, 255);
            profile.MatchPreferences = Models.PersonProfile.CheckingStringLength(profile.MatchPreferences, 255);
            profile.City = Models.PersonProfile.CheckingStringLength(profile.City, 50);
            profile.Country = Models.PersonProfile.CheckingStringLength(profile.Country, 50);


            profileRepo.InsertProfile(profile, username);
            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle(" \u2705 Profile created successfully! \u2705");
            ConsoleUI.ProgramHold();
        }

        // This method basicallys allows user to edit profile fields and save those changes to the database
        public void ModifyProfile(string username)
        {
            bool continueModifying = true;
            while (continueModifying)
            {
                List<Models.PersonProfile> profiles = profileRepo.GetProfilesByUser(username);
                if (profiles.Count == 0)
                {
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle(" \u274C No profiles available to modify. \u274C");
                    ConsoleUI.ProgramHold();
                    return;
                }

                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \uD83D\uDCF1 Pick a profile to edit: \uD83D\uDCF1 ");
                for (int i = 0; i < profiles.Count; i++)
                {
                    ConsoleUI.PutTextInMiddle($"{i + 1}. {profiles[i].Name}");
                }
                string input = ConsoleUI.PutUserInputCenter(" \uD83D\uDCDD Enter profile number (or type 'menu' to go back): \uD83D\uDCDD ");
                if (input.ToLower() == "menu")
                {
                    return;
                }
                if (!int.TryParse(input, out int index) || index < 1 || index > profiles.Count)
                {
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle(" \u274C Incorrect input. \u274C");
                    ConsoleUI.ProgramHold();
                    continue;
                }
                Models.PersonProfile selected = profiles[index - 1];

                bool continueEditing = true;

                bool returnToProfileSelection = false;

                while (continueEditing)
                {
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle(" \uD83C\uDF10 Pick a profile detail you want to edit: \uD83C\uDF10");
                    ConsoleUI.PutTextInMiddle("1. Name" + (!string.IsNullOrEmpty(selected.Name) ? $" (Current: {selected.Name})" : ""));
                    ConsoleUI.PutTextInMiddle("2. Age" + (selected.Age > 0 ? $" (Current: {selected.Age})" : ""));
                    ConsoleUI.PutTextInMiddle("3. Gender" + (!string.IsNullOrEmpty(selected.Gender) ? $" (Current: {selected.Gender})" : ""));
                    ConsoleUI.PutTextInMiddle("4. Interests" + (!string.IsNullOrEmpty(selected.Interests) ? $" (Current: {selected.Interests})" : ""));
                    ConsoleUI.PutTextInMiddle("5. Match Preferences" + (!string.IsNullOrEmpty(selected.MatchPreferences) ? $" (Current: {selected.MatchPreferences})" : ""));
                    ConsoleUI.PutTextInMiddle("6. City" + (!string.IsNullOrEmpty(selected.City) ? $" (Current: {selected.City})" : ""));
                    ConsoleUI.PutTextInMiddle("7. Country" + (!string.IsNullOrEmpty(selected.Country) ? $" (Current: {selected.Country})" : ""));
                    ConsoleUI.PutTextInMiddle("8. Save Changes and Return to Main Menu");
                    ConsoleUI.PutTextInMiddle("9. Save Changes and Return to Profile Selection");

                    string choice = ConsoleUI.PutUserInputCenter(" \uD83D\uDCDD Input your choice: \uD83D\uDCDD");
                    switch (choice)
                    {
                        case "1":
                            selected.Name = Models.PersonProfile.AskForString(" \uD83D\uDCDD Enter new Name: \uD83D\uDCDD");
                            break;
                        case "2":
                            selected.Age = Models.PersonProfile.AskForInt(" \uD83D\uDCDD Enter new Age: \uD83D\uDCDD");
                            break;
                        case "3":
                            selected.Gender = Models.PersonProfile.AskForString(" \uD83D\uDCDD Enter new Gender: \uD83D\uDCDD");
                            break;
                        case "4":
                            selected.Interests = Models.PersonProfile.AskForString(" \uD83D\uDCDD Enter new Interests: \uD83D\uDCDD");
                            break;
                        case "5":
                            selected.MatchPreferences = Models.PersonProfile.AskForString(" \uD83D\uDCDD Enter new Match Preferences: \uD83D\uDCDD");
                            break;
                        case "6":
                            selected.City = Models.PersonProfile.AskForString(" \uD83D\uDCDD Enter new City: \uD83D\uDCDD");
                            break;
                        case "7":
                            selected.Country = Models.PersonProfile.AskForString(" \uD83D\uDCDD Enter new Country: \uD83D\uDCDD");
                            break;
                        case "8":

                            continueEditing = false;
                            break;
                        case "9":

                            returnToProfileSelection = true;
                            continueEditing = false;
                            break;
                        default:
                            ConsoleUI.FreshPage();
                            ConsoleUI.PutTextInMiddle(" \u274C Invalid choice. \u274C");
                            ConsoleUI.ProgramHold();
                            break;
                    }
                }

                selected.Name = Models.PersonProfile.CheckingStringLength(selected.Name, 100);
                selected.Gender = Models.PersonProfile.CheckingStringLength(selected.Gender, 20);
                selected.Interests = Models.PersonProfile.CheckingStringLength(selected.Interests, 255);
                selected.MatchPreferences = Models.PersonProfile.CheckingStringLength(selected.MatchPreferences, 255);
                selected.City = Models.PersonProfile.CheckingStringLength(selected.City, 50);
                selected.Country = Models.PersonProfile.CheckingStringLength(selected.Country, 50);


                profileRepo.UpdateProfile(selected, username);
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \u2705 Profile has been edited successfully! \u2705");
                ConsoleUI.ProgramHold();

                if (returnToProfileSelection)
                {

                    continue;
                }
                else
                {

                    continueModifying = false;
                }
            }
        }

        // This method basically shows all profiles that is associated witht the logged in user 
        public void DisplayProfiles(string username)
        {
            List<Models.PersonProfile> profiles = profileRepo.GetProfilesByUser(username);
            ConsoleUI.FreshPage();
            if (profiles.Count == 0)
            {
                ConsoleUI.PutTextInMiddle(" \u274C Their is 0 profiles available to show. \u274C ");
            }
            else
            {
                foreach (var profile in profiles)
                {
                    ConsoleUI.PutTextInMiddle("------------");
                    ConsoleUI.PutTextInMiddle($"ID: {profile.Id}");
                    ConsoleUI.PutTextInMiddle($"Name: {profile.Name}");
                    ConsoleUI.PutTextInMiddle($"Age: {profile.Age}");
                    ConsoleUI.PutTextInMiddle($"Gender: {profile.Gender}");
                    ConsoleUI.PutTextInMiddle($"Interests: {profile.Interests}");
                    ConsoleUI.PutTextInMiddle($"Match Preferences: {profile.MatchPreferences}");
                    ConsoleUI.PutTextInMiddle($"City: {profile.City}");
                    ConsoleUI.PutTextInMiddle($"Country: {profile.Country}");
                    ConsoleUI.PutTextInMiddle("------------");
                }
            }
            ConsoleUI.ProgramHold();
        }

        // This method basically shows user there profiles allows selection and deletes picked profile from the database.
        public void DeleteProfile(string username)
        {
            while (true)
            {

                List<Models.PersonProfile> profiles = profileRepo.GetProfilesByUser(username);
                if (profiles.Count == 0)
                {
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle(" \u274C Their is 0 profiles available to delete. \u274C");
                    ConsoleUI.ProgramHold();
                    return;
                }


                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \uD83C\uDF10 Select a profile to delete: \uD83C\uDF10");
                for (int i = 0; i < profiles.Count; i++)
                {
                    ConsoleUI.PutTextInMiddle($"{i + 1}. {profiles[i].Name}");
                }


                string input = ConsoleUI.PutUserInputCenter(" \uD83D\uDCDD Enter profile number (or type 'menu' to go back): \uD83D\uDCDD");
                if (input.ToLower() == "menu")
                {
                    return;
                }
                if (!int.TryParse(input, out int choice) || choice < 1 || choice > profiles.Count)
                {
                    ConsoleUI.FreshPage();
                    ConsoleUI.PutTextInMiddle(" \u274C Incorrect input. \u274C");

                    System.Threading.Thread.Sleep(2000);
                    continue;
                }


                Models.PersonProfile selected = profiles[choice - 1];
                profileRepo.DeleteProfile(selected.Id, username);


                ConsoleUI.PutTextInMiddle(" \u274C Do not press anything. \u274C");
                ConsoleUI.PutTextInMiddle("\u2705 Profile deleted successfully. \u2705");
                ConsoleUI.PutTextInMiddle("Redirecting..... ");
                System.Threading.Thread.Sleep(2000);

            }
        }

        // This method basically shows a menu for different search types
        public void SearchProfiles(string username)
        {
            bool inSearchMenu = true;
            while (inSearchMenu)
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \uD83C\uDF10 Search Profiles: \uD83C\uDF10 ");
                ConsoleUI.PutTextInMiddle("1. \uD83D\uDCDD Single detail search \uD83D\uDCDD");
                ConsoleUI.PutTextInMiddle("2. \uD83D\uDCDD Multiple details search \uD83D\uDCDD");
                ConsoleUI.PutTextInMiddle("3. \uD83D\uDCDD Return to Main Menu \uD83D\uDCDD");

                string option = ConsoleUI.PutUserInputCenter(" \uD83D\uDCDD Enter your choice: \uD83D\uDCDD ");
                switch (option)
                {
                    case "1":
                        SingleDetailSearch(username);
                        break;
                    case "2":
                        MultiCriteriaSearch(username);
                        break;
                    case "3":
                        inSearchMenu = false;
                        break;
                    default:
                        ConsoleUI.FreshPage();
                        ConsoleUI.PutTextInMiddle(" \u274C Incorrect input. Press any key to retry. \u274C");
                        ConsoleUI.ProgramHold();
                        break;
                }
            }
        }

        // This method searches profiles based on a user picked profile field
        private void SingleDetailSearch(string username)
        {
            var profiles = profileRepo.GetProfilesByUser(username);
            if (profiles.Count == 0)
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \u274C There is 0 profiles available to search. \u274C");
                ConsoleUI.ProgramHold();
                return;
            }

            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle(" \uD83D\uDD0D Search Profiles by a Single Detail: \uD83D\uDD0D");
            ConsoleUI.PutTextInMiddle("1.  \uD83D\uDCF1 Name \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("2.  \uD83D\uDCF1 Gender \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("3.  \uD83D\uDCF1 Interests \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("4.  \uD83D\uDCF1 Match Preferences \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("5.  \uD83D\uDCF1 City \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("6.  \uD83D\uDCF1 Country \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("7.  \uD83D\uDCF1 Age \uD83D\uDCF1");
            ConsoleUI.PutTextInMiddle("8.  \uD83D\uDCF1 Return to Search Menu \uD83D\uDCF1");

            string choice = ConsoleUI.PutUserInputCenter(" \uD83D\uDCF1 Pick an option \uD83D\uDCF1");
            if (choice == "8")
                return;

            List<Models.PersonProfile> results = new List<Models.PersonProfile>();
            switch (choice)
            {
                case "1":
                    {
                        string term = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter name to search: \uD83D\uDD0D");
                        results = profiles
                            .Where(p => p.Name != null &&
                                        p.Name.Equals(term, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    }
                case "2":
                    {
                        string term = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter gender to search: \uD83D\uDD0D");
                        results = profiles
                            .Where(p => p.Gender != null &&
                                        p.Gender.Equals(term, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    }
                case "3":
                    {
                        string term = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter interests to search: \uD83D\uDD0D");
                        results = profiles
                            .Where(p => Regex.IsMatch(p.Interests ?? "",
                                $@"\b{Regex.Escape(term)}\b",
                                RegexOptions.IgnoreCase))
                            .ToList();
                        break;
                    }
                case "4":
                    {
                        string term = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter match preferences to search: \uD83D\uDD0D");
                        results = profiles
                            .Where(p => Regex.IsMatch(p.MatchPreferences ?? "",
                                $@"\b{Regex.Escape(term)}\b",
                                RegexOptions.IgnoreCase))
                            .ToList();
                        break;
                    }
                case "5":
                    {
                        string term = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter city to search: \uD83D\uDD0D");
                        results = profiles
                            .Where(p => p.City != null &&
                                        p.City.Equals(term, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    }
                case "6":
                    {
                        string term = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter country to search: \uD83D\uDD0D");
                        results = profiles
                            .Where(p => p.Country != null &&
                                        p.Country.Equals(term, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                        break;
                    }
                case "7":
                    {
                        ConsoleUI.FreshPage();
                        ConsoleUI.PutTextInMiddle(" \uD83D\uDD0D Age Search Options: \uD83D\uDD0D ");
                        ConsoleUI.PutTextInMiddle("1.  \uD83D\uDD0D Specific Age \uD83D\uDD0D ");
                        ConsoleUI.PutTextInMiddle("2.  \uD83D\uDD0D Age Range \uD83D\uDD0D");
                        string ageChoice = ConsoleUI.PutUserInputCenter(" \uD83D\uDCF1 Enter your choice: \uD83D\uDCF1");

                        if (ageChoice == "1")
                        {
                            int specificAge = Models.PersonProfile.AskForInt(" \uD83D\uDD0D Enter the specific age to search: \uD83D\uDD0D");
                            results = profiles
                                .Where(p => p.Age == specificAge)
                                .ToList();
                        }
                        else if (ageChoice == "2")
                        {
                            int minAge = Models.PersonProfile.AskForInt(" \uD83D\uDCF1 Enter minimum age: \uD83D\uDCF1");
                            int maxAge = Models.PersonProfile.AskForInt(" \uD83D\uDCF1 Enter maximum age: \uD83D\uDCF1");
                            results = profiles
                                .Where(p => p.Age >= minAge && p.Age <= maxAge)
                                .ToList();
                        }
                        else
                        {
                            ConsoleUI.FreshPage();
                            ConsoleUI.PutTextInMiddle(" \u274C Incorrect age search option. \u274C");
                            ConsoleUI.ProgramHold();
                            return;
                        }
                        break;
                    }
                default:
                    {
                        ConsoleUI.FreshPage();
                        ConsoleUI.PutTextInMiddle(" \u274C Invalid input. Returning to Search Menu. \u274C");
                        ConsoleUI.ProgramHold();
                        return;
                    }
            }


            ConsoleUI.FreshPage();
            if (results.Count > 0)
            {
                ConsoleUI.PutTextInMiddle(" \u2705 Profile match found: \u2705");
                foreach (var profile in results)
                {
                    ConsoleUI.PutTextInMiddle("------------");
                    ConsoleUI.PutTextInMiddle($"ID: {profile.Id}");
                    ConsoleUI.PutTextInMiddle($"Name: {profile.Name}");
                    ConsoleUI.PutTextInMiddle($"Age: {profile.Age}");
                    ConsoleUI.PutTextInMiddle($"Gender: {profile.Gender}");
                    ConsoleUI.PutTextInMiddle($"Interests: {profile.Interests}");
                    ConsoleUI.PutTextInMiddle($"Match Preferences: {profile.MatchPreferences}");
                    ConsoleUI.PutTextInMiddle($"City: {profile.City}");
                    ConsoleUI.PutTextInMiddle($"Country: {profile.Country}");
                    ConsoleUI.PutTextInMiddle("------------");
                }
            }
            else
            {
                ConsoleUI.PutTextInMiddle(" \u274C No profile match found. \u274C");
            }

            ConsoleUI.PutTextInMiddle(" \uD83D\uDD0D Enter any key to go back to the search menu. \uD83D\uDD0D");
            Console.ReadKey(true);
        }

        // This method searches profiles based on a the number of profile fields user has picked and inputted at the same time
        private void MultiCriteriaSearch(string username)
        {
            var profiles = profileRepo.GetProfilesByUser(username);
            if (profiles.Count == 0)
            {
                ConsoleUI.FreshPage();
                ConsoleUI.PutTextInMiddle(" \u274C There is 0 profiles available to search. \u274C");
                ConsoleUI.ProgramHold();
                return;
            }

            ConsoleUI.FreshPage();
            ConsoleUI.PutTextInMiddle(" \uD83D\uDD0D Multi‑Criteria Search: \uD83D\uDD0D");
            string nameCriterion = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter name to search (or leave blank): \uD83D\uDD0D");
            string genderCriterion = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter gender to search (or leave blank): \uD83D\uDD0D");
            string cityCriterion = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter city to search (or leave blank): \uD83D\uDD0D");
            string countryCriterion = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter country to search (or leave blank): \uD83D\uDD0D");
            string interestsCriterion = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter interests to search (or leave blank): \uD83D\uDD0D");
            string matchPrefCriterion = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter match preferences to search (or leave blank): \uD83D\uDD0D");

            string ageInput = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter specific age to search (or leave blank): \uD83D\uDD0D");
            int? specificAge = null;
            if (!string.IsNullOrWhiteSpace(ageInput) && int.TryParse(ageInput, out int ageVal))
                specificAge = ageVal;

            string minAgeInput = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter minimum age (or leave blank): \uD83D\uDD0D");
            int? minAge = null;
            if (!string.IsNullOrWhiteSpace(minAgeInput) && int.TryParse(minAgeInput, out int minVal))
                minAge = minVal;

            string maxAgeInput = ConsoleUI.PutUserInputCenter(" \uD83D\uDD0D Enter maximum age (or leave blank): \uD83D\uDD0D");
            int? maxAge = null;
            if (!string.IsNullOrWhiteSpace(maxAgeInput) && int.TryParse(maxAgeInput, out int maxVal))
                maxAge = maxVal;

            var filtered = profiles.Where(p =>
                (string.IsNullOrWhiteSpace(nameCriterion) || (p.Name != null && p.Name.Equals(nameCriterion, StringComparison.OrdinalIgnoreCase))) &&
                (string.IsNullOrWhiteSpace(genderCriterion) || (p.Gender != null && p.Gender.Equals(genderCriterion, StringComparison.OrdinalIgnoreCase))) &&
                (string.IsNullOrWhiteSpace(cityCriterion) || (p.City != null && p.City.Equals(cityCriterion, StringComparison.OrdinalIgnoreCase))) &&
                (string.IsNullOrWhiteSpace(countryCriterion) || (p.Country != null && p.Country.Equals(countryCriterion, StringComparison.OrdinalIgnoreCase))) &&
                (string.IsNullOrWhiteSpace(interestsCriterion) ||
                    Regex.IsMatch(p.Interests ?? "",
                        $@"\b{Regex.Escape(interestsCriterion)}\b",
                        RegexOptions.IgnoreCase)) &&
                (string.IsNullOrWhiteSpace(matchPrefCriterion) ||
                    Regex.IsMatch(p.MatchPreferences ?? "",
                        $@"\b{Regex.Escape(matchPrefCriterion)}\b",
                        RegexOptions.IgnoreCase)) &&
                (!specificAge.HasValue || p.Age == specificAge.Value) &&
                (!minAge.HasValue || p.Age >= minAge.Value) &&
                (!maxAge.HasValue || p.Age <= maxAge.Value)
            ).ToList();


            ConsoleUI.FreshPage();
            if (filtered.Count > 0)
            {
                ConsoleUI.PutTextInMiddle(" \u2705 Profile matches found: \u2705");
                foreach (var profile in filtered)
                {
                    ConsoleUI.PutTextInMiddle("------------");
                    ConsoleUI.PutTextInMiddle($"ID: {profile.Id}");
                    ConsoleUI.PutTextInMiddle($"Name: {profile.Name}");
                    ConsoleUI.PutTextInMiddle($"Age: {profile.Age}");
                    ConsoleUI.PutTextInMiddle($"Gender: {profile.Gender}");
                    ConsoleUI.PutTextInMiddle($"Interests: {profile.Interests}");
                    ConsoleUI.PutTextInMiddle($"Match Preferences: {profile.MatchPreferences}");
                    ConsoleUI.PutTextInMiddle($"City: {profile.City}");
                    ConsoleUI.PutTextInMiddle($"Country: {profile.Country}");
                    ConsoleUI.PutTextInMiddle("------------");
                }
            }
            else
            {
                ConsoleUI.PutTextInMiddle(" \u274C No profile match found. \u274C ");
            }

            ConsoleUI.PutTextInMiddle(" \uD83D\uDCF1 Input any key to go back to the search menu. \uD83D\uDCF1");
            Console.ReadKey(true);
        }
    }
}

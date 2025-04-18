using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Data;
using Microsoft.Data.SqlClient;
using ConsoleApp84.Models;


namespace ConsoleApp84.Data
{
    // This class is basically carrys out crud procedures in regards to user profiles
    public class ProfileRepository
    {

        // This method basically adds a new profile PersonProfiles table
        public void InsertProfile(PersonProfile profile, string username)
        {
            string sql = @"INSERT INTO PersonProfiles 
                           (UserName, Id, Name, Age, Gender, Interests, MatchPreferences, City, Country)
                           VALUES 
                           (@UserName, @Id, @Name, @Age, @Gender, @Interests, @MatchPreferences, @City, @Country)";


            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", username),
                new SqlParameter("@Id", profile.Id),
                new SqlParameter("@Name", (object)profile.Name ?? DBNull.Value),
                new SqlParameter("@Age", profile.Age > 0 ? (object)profile.Age : DBNull.Value),
                new SqlParameter("@Gender", !string.IsNullOrEmpty(profile.Gender) ? (object)profile.Gender : DBNull.Value),
                new SqlParameter("@Interests", !string.IsNullOrEmpty(profile.Interests) ? (object)profile.Interests : DBNull.Value),
                new SqlParameter("@MatchPreferences", !string.IsNullOrEmpty(profile.MatchPreferences) ? (object)profile.MatchPreferences : DBNull.Value),
                new SqlParameter("@City", !string.IsNullOrEmpty(profile.City) ? (object)profile.City : DBNull.Value),
                new SqlParameter("@Country", !string.IsNullOrEmpty(profile.Country) ? (object)profile.Country : DBNull.Value)
            };

            DatabaseHelper.ExecuteNonQuery(sql, parameters);
        }

        // This method retruns all profile that is associated with the specific user 
        public List<PersonProfile> GetProfilesByUser(string username)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", username)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            List<PersonProfile> profiles = new List<PersonProfile>();
            foreach (DataRow row in dt.Rows)
            {
                PersonProfile profile = new PersonProfile();
                profile.Id = Convert.ToInt32(row["Id"]);
                profile.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                profile.Age = row["Age"] != DBNull.Value ? Convert.ToInt32(row["Age"]) : 0;
                profile.Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : string.Empty;
                profile.Interests = row["Interests"] != DBNull.Value ? row["Interests"].ToString() : string.Empty;
                profile.MatchPreferences = row["MatchPreferences"] != DBNull.Value ? row["MatchPreferences"].ToString() : string.Empty;
                profile.City = row["City"] != DBNull.Value ? row["City"].ToString() : string.Empty;
                profile.Country = row["Country"] != DBNull.Value ? row["Country"].ToString() : string.Empty;
                profiles.Add(profile);
            }
            return profiles;
        }

        // This method updates an existing profile in the database
        public void UpdateProfile(PersonProfile profile, string username)
        {
            string sql = @"UPDATE PersonProfiles 
                           SET Name = @Name, Age = @Age, Gender = @Gender, Interests = @Interests, 
                               MatchPreferences = @MatchPreferences, City = @City, Country = @Country
                           WHERE Id = @Id AND UserName = @UserName";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Name", !string.IsNullOrEmpty(profile.Name) ? (object)profile.Name : DBNull.Value),
                new SqlParameter("@Age", profile.Age > 0 ? (object)profile.Age : DBNull.Value),
                new SqlParameter("@Gender", !string.IsNullOrEmpty(profile.Gender) ? (object)profile.Gender : DBNull.Value),
                new SqlParameter("@Interests", !string.IsNullOrEmpty(profile.Interests) ? (object)profile.Interests : DBNull.Value),
                new SqlParameter("@MatchPreferences", !string.IsNullOrEmpty(profile.MatchPreferences) ? (object)profile.MatchPreferences : DBNull.Value),
                new SqlParameter("@City", !string.IsNullOrEmpty(profile.City) ? (object)profile.City : DBNull.Value),
                new SqlParameter("@Country", !string.IsNullOrEmpty(profile.Country) ? (object)profile.Country : DBNull.Value),
                new SqlParameter("@Id", profile.Id),
                new SqlParameter("@UserName", username)
            };

            DatabaseHelper.ExecuteNonQuery(sql, parameters);
        }

        // this method deleted a profile from database.
        public void DeleteProfile(int profileId, string username)
        {
            string sql = "DELETE FROM PersonProfiles WHERE Id = @Id AND UserName = @UserName";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Id", profileId),
                new SqlParameter("@UserName", username)
            };
            DatabaseHelper.ExecuteNonQuery(sql, parameters);
        }

        // this method looks profiles with same names
        public List<PersonProfile> SearchProfilesByName(string username, string searchTerm)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND Name LIKE @SearchTerm";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", username),
                new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
            };
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            List<PersonProfile> profiles = new List<PersonProfile>();
            foreach (DataRow row in dt.Rows)
            {
                PersonProfile profile = new PersonProfile();
                profile.Id = Convert.ToInt32(row["Id"]);
                profile.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                profile.Age = row["Age"] != DBNull.Value ? Convert.ToInt32(row["Age"]) : 0;
                profile.Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : string.Empty;
                profile.Interests = row["Interests"] != DBNull.Value ? row["Interests"].ToString() : string.Empty;
                profile.MatchPreferences = row["MatchPreferences"] != DBNull.Value ? row["MatchPreferences"].ToString() : string.Empty;
                profile.City = row["City"] != DBNull.Value ? row["City"].ToString() : string.Empty;
                profile.Country = row["Country"] != DBNull.Value ? row["Country"].ToString() : string.Empty;
                profiles.Add(profile);
            }
            return profiles;
        }

        // this method looks for profiles with same gender.
        public List<PersonProfile> SearchProfilesByGender(string username, string searchTerm)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND Gender LIKE @SearchTerm";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method looks for profiles with same interests.
        public List<PersonProfile> SearchProfilesByInterests(string username, string searchTerm)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND Interests LIKE @SearchTerm";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method looks for profiles with same preferences 
        public List<PersonProfile> SearchProfilesByMatchPreferences(string username, string searchTerm)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND MatchPreferences LIKE @SearchTerm";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method looks for profiles with same city
        public List<PersonProfile> SearchProfilesByCity(string username, string searchTerm)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND City LIKE @SearchTerm";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method looks for profiles with the same country
        public List<PersonProfile> SearchProfilesByCountry(string username, string searchTerm)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND Country LIKE @SearchTerm";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@SearchTerm", "%" + searchTerm + "%")
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method looks for profiles with a certain age
        public List<PersonProfile> SearchProfilesByAge(string username, int specificAge)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND Age = @Age";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@Age", specificAge)
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method looks for profiles within an age range
        public List<PersonProfile> SearchProfilesByAgeRange(string username, int minAge, int maxAge)
        {
            string sql = "SELECT * FROM PersonProfiles WHERE UserName = @UserName AND Age >= @MinAge AND Age <= @MaxAge";
            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@UserName", username),
        new SqlParameter("@MinAge", minAge),
        new SqlParameter("@MaxAge", maxAge)
            };
            return ExecuteProfileSearch(sql, parameters);
        }

        // this method is like a helper method that caries out a profile search & maps results 
        private List<PersonProfile> ExecuteProfileSearch(string sql, SqlParameter[] parameters)
        {
            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            List<PersonProfile> profiles = new List<PersonProfile>();
            foreach (DataRow row in dt.Rows)
            {
                PersonProfile profile = new PersonProfile();
                profile.Id = Convert.ToInt32(row["Id"]);
                profile.Name = row["Name"] != DBNull.Value ? row["Name"].ToString() : string.Empty;
                profile.Age = row["Age"] != DBNull.Value ? Convert.ToInt32(row["Age"]) : 0;
                profile.Gender = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : string.Empty;
                profile.Interests = row["Interests"] != DBNull.Value ? row["Interests"].ToString() : string.Empty;
                profile.MatchPreferences = row["MatchPreferences"] != DBNull.Value ? row["MatchPreferences"].ToString() : string.Empty;
                profile.City = row["City"] != DBNull.Value ? row["City"].ToString() : string.Empty;
                profile.Country = row["Country"] != DBNull.Value ? row["Country"].ToString() : string.Empty;
                profiles.Add(profile);
            }
            return profiles;
        }
    }
}

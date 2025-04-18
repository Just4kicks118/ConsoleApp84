using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using ConsoleApp84.Models;

namespace ConsoleApp84.Data
{
    // This class is basically carrys out crud procedures in regards to user account 
    public class UserRepository
    {

        // this method essentially adds a new user to the table of UserAccounts 
        public void AddUser(UserAccount account)
        {
            string sql = "INSERT INTO UserAccounts (Username, Password) VALUES (@Username, @Password)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", account.Username),
                new SqlParameter("@Password", account.Password)
            };

            DatabaseHelper.ExecuteNonQuery(sql, parameters);
        }

        // this method retruns a user account from the UserAccounts table matching the given username
        public UserAccount GetUserByUsername(string username)
        {
            string sql = "SELECT * FROM UserAccounts WHERE Username = @Username";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", username)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(sql, parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                return new UserAccount(row["Username"].ToString(), row["Password"].ToString());
            }
            return null;
        }
    }
}

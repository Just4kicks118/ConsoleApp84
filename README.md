Dating Management System

Welcome to our coursework, a simple console-based application written in C# with a SQL Server database backend. This guide will help you set up, compile, and run the program—even if you have limited technical experience.

Table of Contents

1. What You Need

2. Setting Up the Database

3. Configuring the Project

4. Using the Application

5. Troubleshooting

What You Need

Before you begin, make sure you have:

- Windows 10 or later (or Windows Server) with a graphical interface.

- visual studio 2022 community edition

- SQL Server Express or full edition installed. If you don’t have it, you can get SQL Server Express for free from Microsoft.

- SQL Server Management Studio (SSMS) or another SQL client to run the setup script.



Setting Up the Database

1. Open SQL Server Management Studio (SSMS).

2. Connect to your local SQL Server instance (e.g., localhost\SQLEXPRESS).

3. Create the database and tables:

Open a new query window.

Copy the contents of the sql script provided in the text file.

Click Execute.

This will:

Create a database named DatingAppDBS.

Create two tables: UserAccounts and PersonProfiles, with the necessary columns and relationships.

4. Verify: In the Object Explorer, under Databases, you should see DatingAppDBS. Expand it to confirm the tables exist.

Configuring the Project

1. Clone or download this repository to your computer.

2. Open the folder in File Explorer.

3. Edit the connection string if your SQL Server instance name is different:

Open Data/DatabaseHelper.cs

find the line:
'private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=DatingAppDBSF;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"'

Replace with your server name if needed.

4. Save the file.


Using the Application

1. Sign Up (if you don’t have an account):

Enter 2 and press Enter.

Type a new username and password when prompted.

You’ll see a confirmation message.

2. Login:

From the login menu, enter 1 and press Enter.

Enter your username and password.

If successful, you’ll be taken to the Main Menu.

3. Main Menu Options:

Main Menu:
1. Create Profile
2. Modify Profile
3. Display Profiles
4. Delete Profile
5. Search Profiles
6. Logout
7. Exit

1. Create Profile: Enter personal details (ID, name, age, etc.).

2. Modify Profile: Edit an existing profile.

3. Display Profiles: View all your saved profiles.

4. Delete Profile: Remove a profile from the database.

5. Search Profiles: Find profiles by name, age, city, etc.

6. Logout: Return to the login menu.

7. Exit: Close the application.

4. Navigating Prompts:

Type the number of the menu choice and press Enter.

At any point, type exit (when prompted for text) to quit the application.



Troubleshooting

Cannot connect to database:

Verify SQL Server is running.

Check the connection string in DatabaseHelper.cs.

Make sure the database DatingAppDBSF exists.

Build errors:

Ensure .NET SDK is installed and added to your system PATH.

Run dotnet --version to confirm.

Console window closes immediately:

Run from an existing Command Prompt rather than double-clicking the EXE.

If you still need help, feel free to open an issue or contact the project maintainer.
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO.IsolatedStorage;
using System.IO;
using System.Diagnostics;

namespace CloudService.LoginService
{
    public class UserDB
    {
        private static UserDB instance;

        private List<User> users = new List<User>();

        public List<User> Users
        {
            get { return this.users; } 
        }

        private UserDB() {}

        public static UserDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDB();
                }
                return instance;
            }
        }

        public void LoadUserDB()
        {
            string line = "";

            // DEBUG Reset DB code ...
            if (Cloud.Cloud.Instance.IsoFile.FileExists("UserDB.txt"))
            {
                Debug.WriteLine("UserDB.txt exists, deleting file ...");
                Cloud.Cloud.Instance.IsoFile.DeleteFile("UserDB.txt");
            }
            try
            {
                using (IsolatedStorageFileStream stream =
                    new IsolatedStorageFileStream(
                        "UserDB.txt", FileMode.OpenOrCreate, FileAccess.Read, Cloud.Cloud.Instance.IsoFile
                    )
                )
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        char[] delimiters = new char[] { ':' };
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                            Debug.WriteLine("User: \"" + parts[0] + "\" : \"" + parts[1] + "\" : \"" + parts[2] + "\"");
                            if (parts[0] != null && parts[0].Length > 0
                                && parts[1] != null && parts[1].Length > 0)
                                UserDB.Instance.Users.Add(new User(parts[0], parts[1], Convert.ToInt32(parts[2])));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\nLoadUserDB Failed:\n" + e.StackTrace + "\n");
            }
        }

        public void StoreUser(User user)
        {
            string[] lines = GetUsers();

            char[] delimiters = new char[] { ':' };
            Debug.WriteLine("Contains user? " + ContainsUser(lines, user.UserID) + ".");
            if (ContainsUser(lines, user.UserID))
            {
                // Update already existing user
                Debug.WriteLine("Updating already existing user ...");
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (parts[0].Equals(user.UserID))
                    {
                        parts[2] = Convert.ToString(user.NumCalculations);
                        lines[i] = parts[0] + ":" + parts[1] + ":" + parts[2];
                    }
                }
            }
            using (IsolatedStorageFileStream stream = 
                new IsolatedStorageFileStream(
                    "UserDB.txt", FileMode.Create, FileAccess.Write, Cloud.Cloud.Instance.IsoFile
                )
            )
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                    if (!ContainsUser(lines, user.UserID))
                    {
                        Debug.WriteLine("Adding user to DB ...");
                        writer.WriteLine(user.UserID + ":" + user.Password + ":" + user.NumCalculations);
                    }
                }
            }
        }

        public string[] GetUsers()
        {
            string line = "";
            string[] lines = new string[0];

            using (IsolatedStorageFileStream stream = 
                new IsolatedStorageFileStream(
                    "UserDB.txt", FileMode.Open, FileAccess.Read, Cloud.Cloud.Instance.IsoFile
                )
            )
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    List<string> linesList = new List<string>();
                    while ((line = reader.ReadLine()) != null)
                    {
                        linesList.Add(line);
                    }
                    lines = linesList.ToArray();
                }
            }
            return lines;
        }

        public bool ContainsUser(string[] users, string userID)
        {
            char[] delimiters = new char[] { ':' };
            for (int i = 0; i < users.Length; i++)
            {
                string[] parts = users[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0].Equals(userID))
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUser(string userID)
        {
            for (int i = 0; i < UserDB.Instance.Users.Count; i++)
            {
                if (Users.ElementAt(i).UserID.Equals(userID))
                    return Users.ElementAt(i);
            }
            return null;
        }
    }
}

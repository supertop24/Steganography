using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Steganography.Services;

namespace Steganography.Data
{
    public class Database
    {
        Encryption service = new Encryption();
        public static void Init()
        {
            using (var connection = new SQLiteConnection("Data Source=test1.db;Version=3;"))
            {
                connection.Open();

                // Create users table
                var command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, name TEXT, password TEXT)", connection);
                command.ExecuteNonQuery();
                //Create images table
                command = new SQLiteCommand("CREATE TABLE IF NOT EXISTS images (id INTEGER PRIMARY KEY, name TEXT, date TEXT, image BLOB, userid INTEGER)", connection);
                command.ExecuteNonQuery();
            }
        }
        public void InsertUser(string user, string pass)
        {
            using (var connection = new SQLiteConnection("Data Source=test1.db;Version=3;"))
            {
                connection.Open();
                // Insert a new user
                var encryptPass = service.encryptUser(pass);
                var command = new SQLiteCommand("INSERT INTO users (name,password) VALUES (@user,@pass)", connection);
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@pass",encryptPass );
                command.ExecuteNonQuery();
            }
        }
        public static void InsertImage(string name, byte[] image, int uid)
        {
            using (var connection = new SQLiteConnection("Data Source=test1.db;Version=3;"))
            {
                connection.Open();
                var command = new SQLiteCommand("INSERT INTO images (name, image,date,userid) VALUES (@name, @data,DATE('now'),@uid)", connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@data", image);
                command.Parameters.AddWithValue("@uid", uid);
                command.ExecuteNonQuery();
            }
        }
        public List<User> ReadUser(List<User> users)
        {
            using (var connection = new SQLiteConnection("Data Source=test1.db;Version=3;"))
            {
                connection.Open();
                // Read users
                var command = new SQLiteCommand("SELECT * FROM users", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = reader["name"].ToString();
                        string pass = reader["password"].ToString();
                        User temp = new User(id, name, pass);
                        users.Add(temp);
                    }
                }
            }
            return users;
        }
        public bool userCheck(string user)
        {
            List<User> check = new List<User>();
            ReadUser(check);
            var userFound = check.Find(checkuser => checkuser.Name == user);
            if(userFound!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public User Login(string name, string pass)
        {
            List<User> temp = new List<User>();
            ReadUser(temp);
            var userFind = temp.FirstOrDefault(a => a.Name == name);
            if (userFind!=null&& service.decryptUser(pass,userFind.Password)==true)
            {
                return userFind;
            }
            return null;
        }
        public List<Image> ReadImage(List<Image> images)
        {
            using(var connection = new SQLiteConnection("Data Source=test1.db;Version=3"))
            {
                connection.Open();
                //Read images
                var command = new SQLiteCommand("SELECT * FROM images", connection);
                using(var reader= command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        int id = Convert.ToInt32(reader["id"]);
                        string name = reader["name"].ToString();
                        DateTime date = Convert.ToDateTime(reader["date"]);
                        byte[] image = reader["image"] as byte[];
                        int uid = Convert.ToInt32(reader["userid"]);
                        Image temp = new Image(id, name, date, image, uid);
                        images.Add(temp);
                    }
                }
            }
            return images;
        }
    }
}

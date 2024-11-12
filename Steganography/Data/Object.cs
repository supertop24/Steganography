using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Steganography.Data
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public User(int id, string name, string password)
        {
            ID = id;
            Name = name;
            Password = password;
        }
    }
    public class Image
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        byte[] Images { get; set; }
        public int UserID { get; set; }
        public Image(int id,string name, DateTime date, byte[] image, int uid)
        {
            ID = id;
            Name = name;
            Date = date;
            Images = image;
            UserID = uid;
        }
    }
}

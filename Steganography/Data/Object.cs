using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

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
        public byte[] Images { get; set; }
        public int UserID { get; set; }
        public Image(int id,string name, DateTime date, byte[] image, int uid)
        {
            ID = id;
            Name = name;
            Date = date;
            Images = image;
            UserID = uid;
        }
        public BitmapImage ImageSource
        {
            get
            {
                if (Images != null && Images.Length > 0)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    using (var stream = new MemoryStream(Images))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = stream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }
                    return bitmapImage;
                }
                return null; // or return a default image if necessary
            }
        }
    }
}

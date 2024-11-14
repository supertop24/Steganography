using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Steganography.Data;
using Steganography.ViewModels.Pages;
using Steganography.Services;
using System.Drawing;
using System.Text.RegularExpressions;
using System.IO;

namespace Steganography.Views.Pages
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        Database database = new Database();
        FileDialog file = new FileDialog();
        ImageHandling service = new ImageHandling();
        Encryption encryption = new Encryption();
        string ImagePath = null;
        string Password = null;
        User user;
        Bitmap image;
        string ImageMessage = null;
        public Main(User userPass, string type)
        {
            user = userPass;
            InitializeComponent();
            if (user.ID!=0)
            {
                profile.Content = user.Name;
            }
            ImageDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
            if (type=="e")
            {
                Encode.Visibility = Visibility.Visible;
                Decode.Visibility = Visibility.Collapsed;
                EncodeResult.Visibility = Visibility.Collapsed;
                DecodeResult.Visibility = Visibility.Collapsed;
            }
            else
            {
                Encode.Visibility = Visibility.Collapsed;
                Decode.Visibility = Visibility.Visible;
                EncodeResult.Visibility = Visibility.Collapsed;
                DecodeResult.Visibility = Visibility.Collapsed;
            }
        }
        public Main(BitmapImage bitmapImage, User userPass)
        {
            user = userPass;
            InitializeComponent();
            ImageDisplayR.Source = bitmapImage;
            ImageDisplayD.Source = bitmapImage;
            image = ConvertBitmapImageToBitmap(bitmapImage);
            PathLinkD.Text = "Source from database";
            Encode.Visibility = Visibility.Collapsed;
            Decode.Visibility = Visibility.Visible;
            EncodeResult.Visibility = Visibility.Collapsed;
            DecodeResult.Visibility = Visibility.Collapsed;
        }
        public Bitmap ConvertBitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Encode the BitmapImage into a stream (like a .png or .jpeg)
                BitmapEncoder encoder = new PngBitmapEncoder(); // You can use other encoders like JpegBitmapEncoder
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(memoryStream);

                // Create a Bitmap object from the memory stream
                memoryStream.Seek(0, SeekOrigin.Begin);
                Bitmap bitmap = new Bitmap(memoryStream);
                return bitmap;
            }
        }
        private void embedding()
        {
            string s= file.OpenImagePath();
            Bitmap bitmap = new Bitmap(file.OpenImagePath());
            //service.EncodeText(bitmap, encryption.encryptMessage("I love feli so bad"), "asd");
            bitmap.Save(file.SaveImagePath());
            bitmap = new Bitmap(file.OpenImagePath());
            string text = service.DecodeText(bitmap);
            MessageBox.Show(text);
            decrypt(text);
        }
        private void decrypt(string text)
        {
            if (text.Length >= 4)
            {
                Encryption encryption = new Encryption();
                string result = text.Substring(0, 4);
                if (result == "pass")
                {
                    char fifthChar = text[4];
                    int a = (int)Char.GetNumericValue(fifthChar);
                    Password = text.Length >= a ? text.Substring(text.Length - a) : text;
                    text = text.Substring(5);
                    text = text.Substring(0, text.Length - a);
                    ImageMessage = encryption.decryptMessage(text);
                    DecodePass.Visibility = Visibility.Visible;
                }
                else
                {
                    ImageMessage = encryption.decryptMessage(text);
                }
            }
        }
        private void ChangeMessageDisplay()
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            memory.Seek(0, SeekOrigin.Begin);
            bitmapImage.StreamSource = memory;
            bitmapImage.EndInit();
            ImageDisplay.Source = bitmapImage;//THINGKING NEED OR NOT
        }
        private static byte[] ConvertBitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
        private void ImageDatabase()
        {
            if(user.ID!=0)
            {
                byte[] images = ConvertBitmapToByteArray(image);
                database.InsertImage(user.Name, images, user.ID);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginVM _viewModel = new LoginVM(user);
            if (user.ID==0)
            {
                _viewModel.ClickEventDefinition(new Login(user));
                MessageBox.Show("Please create an account to access personal profile!");
            }
            else
            {
                _viewModel.ClickEventDefinition(new Profile(user));
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginVM _viewModel = new LoginVM(user);
            _viewModel.ClickEventDefinition(new Login(user));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ImagePath= file.OpenImagePath();
            if(ImagePath!=null)
            {
                PathLink.Text = ImagePath;
                PathLinkD.Text = ImagePath;
                ImageDisplay.Source = new BitmapImage(new Uri(ImagePath));
                ImageDisplayE.Source = new BitmapImage(new Uri(ImagePath));
                ImageDisplayD.Source = new BitmapImage(new Uri(ImagePath));
                ImageDisplayR.Source = new BitmapImage(new Uri(ImagePath));
            }
        }
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(TogglePass.IsChecked==true)
            {
                TogglePass.Background = new SolidColorBrush(Colors.Green);
                PassBar.Visibility = Visibility.Visible;
            }
            else if (TogglePass.IsChecked == false)
            {
                TogglePass.Background = new SolidColorBrush(Colors.White);
                PassBar.Visibility = Visibility.Collapsed;
            }
        }
        private void changeViewEncode( )
        {
            ImageDatabase();
            MessageBox.Show("Encoding Successful!");
            Encode.Visibility = Visibility.Collapsed;
            EncodeResult.Visibility = Visibility.Visible;
            EncodeMessage.Text = MessageImage.Text;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(ImagePath!=null)
            {
                if(string.IsNullOrWhiteSpace(MessageImage.Text)==false)
                {
                    if(TogglePass.IsChecked==true)
                    {
                        string passwordPattern = @"^\d{4,8}$";
                        if (Regex.IsMatch(PassBar.Password, passwordPattern) == true)
                        {
                            image = new Bitmap(ImagePath);
                            service.EncodeTextPass(image,encryption.encryptMessage(MessageImage.Text),PassBar.Password);
                            changeViewEncode();
                        }
                        else
                        {
                            MessageBox.Show("Please insert the password only number minimal 4 digits, maximal 8 digits!");
                        }
                    }
                    else
                    {
                        image = new Bitmap(ImagePath);
                        service.EncodeText(image, encryption.encryptMessage(MessageImage.Text));
                        changeViewEncode();
                    }
                }
                else
                {
                    MessageBox.Show("Please insert the message!");
                }
            }
            else
            {
                MessageBox.Show("Please select an image!");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            image.Save(file.SaveImagePath());
        }
        private void DecodeResultDisplay()
        {
            DecodeMessage.Text = ImageMessage;
            Decode.Visibility = Visibility.Collapsed;
            DecodeResult.Visibility = Visibility.Visible;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if(ImagePath!=null||image!=null)
            {
                if(image!=null)
                {
                    string t = service.DecodeText(image);
                    decrypt(t);
                    if (Password != null && Password == DecodePassbar.Password)
                    {
                        DecodeResultDisplay();
                    }
                    else if (Password == null)
                    {
                        DecodeResultDisplay();
                    }
                    else
                    {
                        MessageBox.Show("Please insert correct password!");
                    }
                }
                else if(ImagePath!=null)
                {
                    Bitmap bitmap = new Bitmap(ImagePath);
                    string text = service.DecodeText(bitmap);
                    decrypt(text);
                    if (Password != null && Password == DecodePassbar.Password)
                    {
                        DecodeResultDisplay();
                    }
                    else if (Password == null)
                    {
                        DecodeResultDisplay();
                    }
                    else
                    {
                        MessageBox.Show("Please insert correct password!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please insert image!");
            }
        }
    }
}

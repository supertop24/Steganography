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
using System.Text.RegularExpressions;

namespace Steganography.Views.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private readonly Database database;
        private readonly LoginVM _viewModel;
        private User userCheck;
        public Login(User user)
        {
            InitializeComponent();
            userCheck = user;
            _viewModel = new LoginVM(user);
            database = new Database();
            LoginForm.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userCheck.ID==0)
            {
                LoginForm.Visibility = Visibility.Visible;
                usernameLogin.Text = string.Empty;
                passLogin.Password = string.Empty;
            }
            else
            {
                _viewModel.ClickEventDefinition(new Profile(userCheck));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginForm.Visibility = Visibility.Collapsed;
            SignupForm.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            LoginForm.Visibility = Visibility.Collapsed;
            SignupForm.Visibility = Visibility.Visible;
            usernameSignUP.Text = string.Empty;
            passSignUp.Password = string.Empty;
            repassSignUp.Password = string.Empty;
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if(usernameLogin.Text!=string.Empty&& passLogin.Password!= string.Empty)
            {
                var userCheck = database.Login(usernameLogin.Text, passLogin.Password);
                if (userCheck == null)
                {
                    MessageBox.Show("User is not found!");

                }
                else
                {
                    _viewModel.ClickEventDefinition(new Login(userCheck));
                    profile.Content = userCheck.Name;
                    LoginForm.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Welcome " + userCheck.Name + "!");
                    _viewModel.ClickEventDefinition(new Login(userCheck));
                }
            }
            else
            {
                MessageBox.Show("Please fill the login form!");
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            string usernamePattern = @"^[A-Za-z\d]{3,8}$";
            if(usernameSignUP.Text!= string.Empty && passSignUp.Password!= string.Empty && repassSignUp.Password!= string.Empty)
            {
                if (Regex.IsMatch(usernameSignUP.Text, usernamePattern) == true)
                {
                    if (database.userCheck(usernameSignUP.Text) == false)
                    {
                        string passwordPattern = @"^(?=.*\d)[A-Za-z\d]{8,}$";
                        string passCheck = passSignUp.Password;
                        if (Regex.IsMatch(passCheck, passwordPattern)==true)
                        {
                            if (passSignUp.Password == repassSignUp.Password)
                            {
                                database.InsertUser(usernameSignUP.Text, passSignUp.Password);
                                MessageBox.Show("Account have been created!");
                                SignupForm.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                repassSignUp.Password = string.Empty;
                                MessageBox.Show("Please reconfirm the password!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password minimal 8 characters with 1 number without special characters or space allowed!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username has been taken!");
                    }
                }
                else
                {
                    MessageBox.Show("Username minimal 3 characters and maximal 8 characters!\nNo special characters or space allowed!");
                }
            }
            else
            {
                MessageBox.Show("Please fill the signup form!");
            }
            
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)//encryption
        {
            string type = "e";//E for encryption
            _viewModel.ClickEventDefinition(new Main(userCheck, type));
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)//Decryption
        {

            string type = "d";//E for encryption
            _viewModel.ClickEventDefinition(new Main(userCheck, type));
        }
    }
}

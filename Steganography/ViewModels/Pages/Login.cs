using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Steganography.Data;
using Steganography.Services;
using System.Windows;
using System.Security.Cryptography;
using System.IO;
using System.Drawing;
using FileDialog = Steganography.Services.FileDialog;
using System.Windows.Controls;

namespace Steganography.ViewModels.Pages
{
    public class LoginVM:ViewModelBaseClass 
    {
        public User userLogin;
        public void ClickEventDefinition(Page destinationPage)
        {
            MainWindowVM mainVM = (MainWindowVM)Application.Current.MainWindow.DataContext;
            mainVM.CurrentDisplayPage = destinationPage;
        }
        public LoginVM(User user)
        {
            

        }
    }
}

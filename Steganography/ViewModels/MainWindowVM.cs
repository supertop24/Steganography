using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Steganography.Views;
using Steganography.Views.Pages;
using System.Data.SQLite;
using Steganography.Data;


namespace Steganography.ViewModels
{
    class MainWindowVM : ViewModelBaseClass
    {
        public MainWindowVM()
        {
            Database.Init();
            User user = new User(0,null, null); 
            CurrentDisplayPage = new Login(user);
        }

        private Page _currentDisplayPage;
        public Page CurrentDisplayPage
        {
            get
            {
                return _currentDisplayPage;
            }

            set
            {
                _currentDisplayPage = value;
                OnPropertyChanged("CurrentDisplayPage");
            }
        }
    }
}

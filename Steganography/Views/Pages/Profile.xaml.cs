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
using Image = Steganography.Data.Image;
using Steganography.Services;

namespace Steganography.Views.Pages
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Page
    {
        public ICommand SaveItemCommand { get; set; }
        public ICommand DecodeItemCommand { get; set; }
        public List<Image> Selected;
        Database database;
        LoginVM _viewModel;
        User user;
        public Profile(User userPass)
        {
            Selected = new List<Image>();
            user = userPass;
            database = new Database();
            _viewModel = new LoginVM(user);
            Selected=database.Selected(Selected, user.ID);
            InitializeComponent();
            DataContext = this;
            List.ItemsSource = Selected;
            SaveItemCommand = new RelayCommand(SaveItem);
            DecodeItemCommand = new RelayCommand(DecodeItem);
        }

        private void profile_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClickEventDefinition(new Profile(user));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.ClickEventDefinition(new Login(user));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User logout = new User(0, null, null);
            _viewModel.ClickEventDefinition(new Login(logout));
        }
        private void SaveItem(object parameter)
        {
            FileDialog file = new FileDialog();
            var selectedItem = parameter as Image;
            file.SaveImage(selectedItem.Images);
        }
        private void DecodeItem(object parameter)
        {
            var selectedItem = parameter as Image;
            _viewModel.ClickEventDefinition(new Main(selectedItem.ImageSource, user));
        }
    }
}

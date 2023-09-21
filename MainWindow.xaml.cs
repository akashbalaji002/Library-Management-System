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

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void userLogin_btn_Click(object sender, RoutedEventArgs e)
        {
              
        }

        private void adminLogin_btn_Click(object sender, RoutedEventArgs e)
        {
            AdminDashBoard obj = new AdminDashBoard();
            obj.Show();
        }

        private void userLogin_btn_Click_1(object sender, RoutedEventArgs e)
        {
            userPage userpage = new userPage();
            userpage.Show();
        }

        private void exitApp_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

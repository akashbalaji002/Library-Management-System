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
using System.Windows.Shapes;

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for adminLoginPage.xaml
    /// </summary>
    public partial class adminLoginPage : Window
    {
        public adminLoginPage()
        {
            InitializeComponent();
        }

        private void Adminlogin_btn_Click(object sender, RoutedEventArgs e)
        {
            if (AdminLoginUserId_textBox.Text == "admin" && AdminLoginPassword_passBox.Password == "Admin@1234")
            {
                AdminSession.Login(); 
                MessageBox.Show("Congratulations!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                AdminDashBoard obj = new AdminDashBoard();
                obj.Show();
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Invalid Credentials", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginUserId_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}

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
    /// Interaction logic for userPage.xaml
    /// </summary>
    public partial class userPage : Window
    {
        public userPage()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
           
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            newUserPage newUser = new newUserPage();
            newUser.Show();
        }

        private void exuser_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            exUserLoginPage ex  = new exUserLoginPage();
            ex.Show();
        }
    }
}

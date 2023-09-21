using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for AdminDashBoard.xaml
    /// </summary>
    public partial class AdminDashBoard : Window
    {
        public AdminDashBoard()
        {
            InitializeComponent();
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }
       
        private void addBook_Click(object sender, RoutedEventArgs e)
        {
            addBooksPage obj = new addBooksPage();
            obj.Show();
        }

     

        private void logout_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you want to logout ?","",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(res == MessageBoxResult.Yes) 
            {
                this.Hide();
                MessageBox.Show("You have Logged out !", "Logged Out", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void viewUser_Click(object sender, RoutedEventArgs e)
        {
            viewPageUsers obj = new viewPageUsers();
            obj.Show();
        }
        
        private void viewBook_Click(object sender, RoutedEventArgs e)
        {
            viewPageBooks obj = new viewPageBooks();
            obj.Show();
        }

        private void IssueBooks_Click(object sender, RoutedEventArgs e)
        {
            IssueBookPage obj = new IssueBookPage();
            obj.Show();
        }

        private void viewIssuedBook_Click(object sender, RoutedEventArgs e)
        {
            viewIssuedBooksPage obj = new viewIssuedBooksPage();
            obj.Show();
        }

        private void returnBook_Click(object sender, RoutedEventArgs e)
        {
            returnBookPage obj = new returnBookPage();
            obj.Show();
        }

        private void aboutPage_Click(object sender, RoutedEventArgs e)
        {
            AboutPage obj = new AboutPage();    
            obj.Show();
        }
    }
}

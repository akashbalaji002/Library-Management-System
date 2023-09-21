using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Text.RegularExpressions;

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for exUserLoginPage.xaml
    /// </summary>
    public partial class exUserLoginPage : Window
    {
        public static int temp = 0;
        
        public exUserLoginPage()
        {
            InitializeComponent();
        }
        public bool isEmpty()
        {
            if (LoginUserId_textBox.Text == "" || LoginPassword_passBox.Password == "")
            {
                return true;
            }
            return false;
        }
        
        public bool LoginSuccess()
        {
            temp = Convert.ToInt32(LoginUserId_textBox.Text);
            bool loginSuccessful = false;
            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "Select * from user_details where user_id='" + int.Parse(LoginUserId_textBox.Text.Trim()) + "' and user_pass = '" + LoginPassword_passBox.Password.Trim()+"'";
            SqlDataAdapter da = new SqlDataAdapter(myquery, mycon);
     
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                loginSuccessful = true;
            }
            return loginSuccessful;
        }

       

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isEmpty())
            {
                MessageBox.Show("Please provide Login Credentials", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (LoginSuccess())
                {
                    this.Hide();
                    MessageBox.Show("Congratulations! , Logged In Successfully", "Welcome", MessageBoxButton.OK, MessageBoxImage.Information);
                    userDashBoard obj = new userDashBoard();
                    obj.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Login Credentials", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LoginUserId_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

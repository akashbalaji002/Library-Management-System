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
    /// Interaction logic for viewPageUsers.xaml
    /// </summary>
    public partial class viewPageUsers : Window
    {

        public viewPageUsers()
        {
            InitializeComponent();
        }
        private DataTable tab = new DataTable();
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        private void ViewUsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadGrid();
        }
        public bool isUserAvailable()
        {

            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "select count(*) from user_details where user_id='" + search_UserTxtBox.Text + "'";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand(myquery, con);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                return true;
            }
            con.Close();
            return false;

        }
        public void LoadSearchUser()
        {
            if (isUserAvailable())
            {
                SqlCommand cmd = new SqlCommand("select * from user_details where user_id="+Convert.ToInt32(search_UserTxtBox.Text), con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                con.Close();
                ViewUsersGrid.ItemsSource = dt.DefaultView;
            }
            else
            {
                MessageBox.Show("User not found", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                search_UserTxtBox.Clear();
                search_UserTxtBox.Focus();
            }

        }

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from user_details", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            tab.Load(dr);
            con.Close();
            ViewUsersGrid.ItemsSource = tab.DefaultView;
        }

        private void viewUser_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void searchUsers_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadSearchUser();
        }

        private void searchUsers_btn_Click_1(object sender, RoutedEventArgs e)
        {

        }
        
        private void search_UserTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dv = tab.DefaultView;
            dv.RowFilter = "fullname Like '%" + search_UserTxtBox.Text + "%'";
        }
    }
}

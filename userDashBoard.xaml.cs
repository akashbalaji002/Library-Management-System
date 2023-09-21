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
using System.Data.SqlClient;
using System.Data;
using System.Windows.Markup;

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for userDashBoard.xaml
    /// </summary>
    public partial class userDashBoard : Window 
    {
        
       private DataTable booksTable = new DataTable();
        public userDashBoard()
        {
            InitializeComponent();
         
        }
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select user_id,fullname,addres,ph_no from user_details where user_id="+exUserLoginPage.temp,con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            viewGridData.ItemsSource = dt.DefaultView;
        }
        private void viewUserDetails_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void UpdateDetails_Click(object sender, RoutedEventArgs e)
        {
            userUpdatePage userUpdatePage = new userUpdatePage();
            userUpdatePage.Show();  
        }

        private void DeleteDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from user_details where user_id=" +exUserLoginPage.temp , con);

                    MessageBoxResult res = MessageBox.Show("Are you sure want to delete your profile ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (res == MessageBoxResult.Yes)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your profile has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
           
        }

        private void History_click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT  i.user_id,  i.Book_Id,  i.Book_Name, i.issueDate,  i.returnDate,  b.Author_Name,   b.Publisher_Name,  b.Book_Price,    b.Number_Of_Pages,  b.Published_Date FROM   issueBook AS i JOIN     book_details AS b ON    i.Book_Id = b.Book_Id WHERE  i.user_id =" + exUserLoginPage.temp, con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            bookHistoryDataGrid.ItemsSource = dt.DefaultView;
        }

        private void viewGridData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void viewBooks_btn_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from book_details", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            booksTable.Load(dr);
            con.Close();
            viewBookData.ItemsSource = booksTable.DefaultView;
        }

        private void search_book_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataView dv = booksTable.DefaultView;
            dv.RowFilter="Book_Name Like '%"+search_bookTxtBox.Text+"%'";
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            AboutPage obj = new AboutPage();    
            obj.Show();
        }
    }

    
}

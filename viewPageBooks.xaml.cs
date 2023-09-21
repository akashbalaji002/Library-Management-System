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
using System.Windows.Media.Media3D;

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for viewPageBooks.xaml
    /// </summary>
    public partial class viewPageBooks : Window
    {
        public viewPageBooks()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from book_details", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            ViewBooksGrid.ItemsSource = dt.DefaultView;
            
        }
        
        private void ViewBooksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadGrid();
        }
        public int bookCount()
        {
            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "SELECT COUNT(*) FROM book_details"; 
            SqlConnection con = new SqlConnection(mycon);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(myquery, con);
                int cnt = (int)cmd.ExecuteScalar();
                return cnt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        private void loadData_OnGrid_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            total_books_label.Content = bookCount().ToString();
            if (ViewBooksGrid.Items.Count <= 0)
            {
               
                MessageBox.Show("No Books found","", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
            
        }
        public bool isvalid()
        {
            if(search_bookId_textBox.Text=="")
            {
                MessageBox.Show("Please enter the Book Id to delete book","Failed",MessageBoxButton.OK,MessageBoxImage.Error);
                search_bookId_textBox.Focus();
                return false;
            }
            return true;
        }

        public static int i = 0;
        public bool isBookAvailable()
        {
           
            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "select count(*) from book_details where Book_Id='" + search_bookId_textBox.Text+"'";
            SqlConnection con = new SqlConnection(mycon);
            con.Open();
            SqlCommand cmd = new SqlCommand(myquery,con);
            int count = (int)cmd.ExecuteScalar();
            if(count>0)
            {
                return true;
            }
            con.Close();
            return false;
         
        }
       
        private void Bookdel_btn_Click(object sender, RoutedEventArgs e)
        {
           
            if (isvalid())
            {  
                try
                {
                    if(isBookAvailable())
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("delete from book_details where Book_Id=" + search_bookId_textBox.Text, con);
                        
                        MessageBoxResult res = MessageBox.Show("Are you sure want to delete this book ?","",MessageBoxButton.YesNo,MessageBoxImage.Question);
                        if(res== MessageBoxResult.Yes)
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Book has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                            
                        }
                        else
                        {
                            search_bookId_textBox.Clear();
                        }
                       
                    }
                    else
                    {
                        
                        MessageBox.Show("Book Not Available", "Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);

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
        }

        private void UpdateBookDetails_btn_Click(object sender, RoutedEventArgs e)
        {

            if(search_bookId_textBox.Text=="")
            {
                MessageBox.Show("Please enter Book Id for Updation","Failed",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                if (isBookAvailable())
                {
                    i = Convert.ToInt32(search_bookId_textBox.Text);
                    BookUpdatePage obj = new BookUpdatePage();
                    obj.Show();
                }
                else
                {
                    MessageBox.Show("Book Not Available for Updation", "Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                
            }

        }

        

        private void add_book_Click(object sender, RoutedEventArgs e)
        {
            addBooksPage obj = new addBooksPage();
            obj.Show();
            this.Close();
        }
    }
}

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
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for addBooksPage.xaml
    /// </summary>
    public partial class addBooksPage : Window
    {
        public addBooksPage()
        {
            InitializeComponent();
        }

        static string s = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
        static SqlConnection con = new SqlConnection(s);

        private void authorName_txtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void cancel_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        public bool isInputValid()
        {
            if (!bookId_txtBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Book Id should be Numeric", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookId_txtBox.Clear();
                bookId_txtBox.Focus();
                return false;
            }

            string ss = "[~!@#$%^&*()_+{}\\[\\]:;,.<>/?-]";
            if (!(bookId_txtBox.Text.All(char.IsDigit)) && Regex.IsMatch(bookName_txtBox.Text, ss) == true)
            {
                MessageBox.Show("Enter a Valid Name", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookName_txtBox.Clear();
                bookName_txtBox.Focus();
                return false;
            }

            string auth = "[~!@#$%^&*()_+{}\\[\\]:;,.<>/?-]";
            if (!(authorName_txtBox.Text.All(char.IsDigit)) && Regex.IsMatch(authorName_txtBox.Text, auth) == true)
            {
                MessageBox.Show("Enter a Valid Author Name", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                authorName_txtBox.Clear();
                authorName_txtBox.Focus();
                return false;
            }


            if (!NoOfPages_txtBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Pages should be Numeric", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                NoOfPages_txtBox.Clear();
                NoOfPages_txtBox.Focus();
                return false;
            }

            if (!bookPrice_txtBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Price should be Numeric", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookPrice_txtBox.Clear();
                bookPrice_txtBox.Focus();
                return false;
            }
            if (!book_qty_txtBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Quantity should be Numeric", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                book_qty_txtBox.Clear();
                book_qty_txtBox.Focus();
                return false;
            }


            return true;
        }

        public bool isEmpty()
        {
            if (bookId_txtBox.Text == "")
            {
                MessageBox.Show("Book Id is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookId_txtBox.Focus();
                return false;
            }
            if (bookName_txtBox.Text == "")
            {
                MessageBox.Show("Book Name is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookName_txtBox.Focus();
                return false;
            }
            if (authorName_txtBox.Text == "")
            {
                MessageBox.Show("Author Name is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                authorName_txtBox.Focus();
                return false;
            }
            if (bookPub_txtBox.Text == "")
            {
                MessageBox.Show("Book Publisher Name is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookPub_txtBox.Focus();
                return false;
            }
            if (NoOfPages_txtBox.Text == "")
            {
                MessageBox.Show("Pages is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                NoOfPages_txtBox.Focus();
                return false;
            }

            if (bookPrice_txtBox.Text == "")
            {
                MessageBox.Show("Book Publisher Name is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                bookPrice_txtBox.Focus();
                return false;
            }
            if(book_qty_txtBox.Text=="")
            {
                MessageBox.Show("Book Quantity is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                book_qty_txtBox.Focus();
                return false;
            }
            if (PublishDate_picker.Text == "")
            {
                MessageBox.Show("Publish Date is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                PublishDate_picker.Focus();
                return false;
            }
            return true;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((isEmpty() && isInputValid())==true)
            {
               try
                {
                    string query = "insert into book_details values(@Book_Id,@Book_Name,@Author_Name,@Publisher_Name,@Number_Of_Pages,@Book_Price,@Published_Date,@Available_Quantity)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text; 
                    cmd.Parameters.AddWithValue("@Book_Id", bookId_txtBox.Text);
                    cmd.Parameters.AddWithValue("@Book_Name", bookName_txtBox.Text);
                    cmd.Parameters.AddWithValue("@Author_Name", authorName_txtBox.Text);
                    cmd.Parameters.AddWithValue("@Publisher_Name", bookPub_txtBox.Text);
                    cmd.Parameters.AddWithValue("@Number_Of_Pages", NoOfPages_txtBox.Text);
                    cmd.Parameters.AddWithValue("@Book_Price", bookPrice_txtBox.Text);
                    cmd.Parameters.AddWithValue("@Published_Date", PublishDate_picker.SelectedDate);
                    cmd.Parameters.AddWithValue("@Available_Quantity",book_qty_txtBox.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show(bookName_txtBox.Text + " Added Successfully ", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);


                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.Message);
                }
               this.Hide();
            }
        }
    }
}

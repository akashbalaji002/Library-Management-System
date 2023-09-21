using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for IssueBookPage.xaml
    /// </summary>
    public partial class IssueBookPage : Window
    {
        public IssueBookPage()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        public void LoadUserDetails()
        {
            try
            {
                con.Open();
                string myquery = "select fullname,addres,ph_no from user_details where user_id='" + UserIdTxtBox_toIssueBook.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    userName_label.Content = "Name : "+sdr["fullname"].ToString();
                    Address_label.Content = "Address : "+sdr["addres"].ToString();
                    ph_label.Content = "Phone : "+sdr["ph_no"].ToString();
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"Failed",MessageBoxButton.OK, MessageBoxImage.Warning);
            }           
            finally
            { 
                con.Close();
            }    
        }
        public void LoadBookDetails()
        {
            try
            {
                con.Open();
                string myquery = "select Book_Name,Author_Name,Publisher_Name,Book_Price,Published_Date from book_details where Book_Id='" + bookIdTxtBox_ToIssueBook.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    bookName_lbl.Content = "Book Name : "+sdr["Book_Name"].ToString();
                    authorName_lbl.Content = "Author Name : "+sdr["Author_Name"].ToString();
                    pubName_lbl.Content = "Publisher Name : "+sdr["Publisher_Name"].ToString();
                    bookPrice_lbl.Content ="Book Price : "+ sdr["Book_Price"].ToString();
                    publishDate_lbl.Content = "Published Date : " +sdr["Published_Date"].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            { 
                con.Close();
            }
        }
       

        private void Show_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UserIdTxtBox_toIssueBook.Text != "" && bookIdTxtBox_ToIssueBook.Text != "")
            {
                LoadUserDetails();
                LoadBookDetails();
            }
            else
            {
                MessageBox.Show("Please Provide User Id and Book Id", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public  string GetBookName()
        {
             string temp = "";
            try
            {
                con.Open();
                string myquery = "SELECT Book_Name FROM book_details WHERE Book_Id ="+ bookIdTxtBox_ToIssueBook.Text;
                SqlCommand cmd = new SqlCommand(myquery, con);
                
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    temp = sdr["Book_Name"].ToString();
                    bookName_ToIssueBook.Text = temp;
                }
                else
                {
                    MessageBox.Show("Book not found");
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
            return temp;
        }


        public bool isEmpty()
        {
            if(UserIdTxtBox_toIssueBook.Text=="" || bookIdTxtBox_ToIssueBook.Text=="" || bookName_ToIssueBook.Text=="" || issueDate_Picker.Text=="" || returnDate_Picker.Text=="")
            {
                return true;
            }
            return false;
        }
     
 
       
        private void IssueBook_btn_Click(object sender, RoutedEventArgs e)
        {
           
           
            string tmp = GetBookName().Trim();
            if (bookName_ToIssueBook.Text == tmp)
            {
                if (isEmpty() == false)
                {
                    try
                    {
                        string query = "insert into issueBook values(@user_id,@Book_Id,@Book_Name,@issueDate,@returnDate)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@user_id", UserIdTxtBox_toIssueBook.Text);
                        cmd.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToIssueBook.Text);
                        cmd.Parameters.AddWithValue("@Book_Name", GetBookName());
                        cmd.Parameters.AddWithValue("issueDate", issueDate_Picker.SelectedDate);
                        cmd.Parameters.AddWithValue("@returnDate", returnDate_Picker.SelectedDate);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        IssueBook();
                        MessageBox.Show("Book Issued","Success",MessageBoxButton.OK,MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error : "+ex.Message);
                    }
                    finally
                    {
                        con.Close(); 
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please provide all the details to issue a book", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Book Id and Book Name is not matching", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void bookName_ToIssueBook_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void IssueBook()
        {
            string selectQuantityQuery = "SELECT Available_Quantity FROM book_details WHERE Book_Id ="+bookIdTxtBox_ToIssueBook.Text;

            using (con)
            {
                con.Open();

                using (SqlCommand selectQuantityCmd = new SqlCommand(selectQuantityQuery, con))
                {
                    selectQuantityCmd.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToIssueBook.Text);
                    int currentQuantity = (int)selectQuantityCmd.ExecuteScalar();

                    if (currentQuantity > 0)
                    {
                        int newQuantity = currentQuantity - 1;

                        string updateQuantityQuery = "UPDATE book_details SET Available_Quantity ="+ newQuantity +" WHERE Book_Id ="+bookIdTxtBox_ToIssueBook.Text;

                        using (SqlCommand updateQuantityCmd = new SqlCommand(updateQuantityQuery, con))
                        {
                            updateQuantityCmd.Parameters.AddWithValue("@Available_Quantity", newQuantity);
                            updateQuantityCmd.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToIssueBook.Text);
                            updateQuantityCmd.ExecuteNonQuery();
                        }

                    }
                    else
                    {
                        MessageBox.Show("No available copies of this book.");
                    }
                }
            }
        }





    }
}

using System;
using System.Collections.Generic;
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
 
    public partial class returnBookPage : Window
    {
        public returnBookPage()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        public bool isEmpty()
        {
            if (UserIdTxtBox_toReturnBook.Text == "" || bookIdTxtBox_ToReturnBook.Text == "")
            {
                return true;
            }
            return false;
        }
        public DateTime GetIssueDate()
        {
            DateTime issueDate = DateTime.MinValue;
            try
            {
                con.Open();
                string q = "SELECT issueDate FROM issueBook WHERE user_id = " + UserIdTxtBox_toReturnBook.Text + " AND Book_id = " + bookIdTxtBox_ToReturnBook.Text;
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    if (!sdr.IsDBNull(sdr.GetOrdinal("issueDate")))
                    {
                        issueDate = sdr.GetDateTime(sdr.GetOrdinal("issueDate"));
                    }
                }
                else
                {
                    MessageBox.Show("No data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return issueDate;
        }

        public DateTime GetReturnDate()
        {
            DateTime returnDate = DateTime.MinValue;
            try
            {
                con.Open();
                string q = "SELECT returnDate FROM issueBook WHERE user_id = " + UserIdTxtBox_toReturnBook.Text + " AND Book_id = " + bookIdTxtBox_ToReturnBook.Text;
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    if (!sdr.IsDBNull(sdr.GetOrdinal("returnDate")))
                    {
                        returnDate = sdr.GetDateTime(sdr.GetOrdinal("returnDate"));
                    }
                }
                else
                {
                    MessageBox.Show("No data found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return returnDate;
        }
        public static  double penaltyAmount = 0;
        public static string s="";

        private void ReturnBook_btn_Click(object sender, RoutedEventArgs e)
        {
            DateTime issueDate = GetIssueDate(); 
            DateTime currentDate = DateTime.Now;
            DateTime dueDate = issueDate.AddDays(30); 

            if (currentDate > dueDate)
            {
                int daysLate = (int)(currentDate - dueDate).TotalDays;
                penaltyAmount = daysLate * 1.0; 

                MessageBox.Show($"Book is {daysLate} days late. Penalty Amount: {penaltyAmount}", "Late Return Penalty", MessageBoxButton.OK, MessageBoxImage.Information);

                paymentPage obj = new paymentPage();
                bool isPaid = obj.isPaid();

                if (isPaid)
                {
                    returnBook();
                }
                else
                {
                    obj.ShowDialog(); 
                }
            }
            else
            {
                returnBook();
            }
        }






        private void Show_btn_Click(object sender, RoutedEventArgs e)
        {
            if (UserIdTxtBox_toReturnBook.Text != "" && bookIdTxtBox_ToReturnBook.Text != "")
            {
                LoadUserDetails();
                LoadBookDetails();
            }
            else
            {
                MessageBox.Show("Please Provide User Id and Book Id", "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void LoadUserDetails()
        {
            try
            {
                con.Open();
                string myquery = "select fullname,addres,ph_no from user_details where user_id='" + UserIdTxtBox_toReturnBook.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    userName_label.Content = "Name : " + sdr["fullname"].ToString();
                    Address_label.Content = "Address : " + sdr["addres"].ToString();
                    ph_label.Content = "Phone : " + sdr["ph_no"].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                con.Close();
            }
            try
            {
                con.Open();
                string q = "select issueDate,returnDate from issueBook where user_id=" + UserIdTxtBox_toReturnBook.Text + " AND Book_Id=" + bookIdTxtBox_ToReturnBook.Text;
                SqlCommand cmd = new SqlCommand(q, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    issueDate_lbl.Content = "Issued Date : " + sdr["issueDate"];
                    returnDate_lbl.Content = "Due Date : " + sdr["returnDate"];

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
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
                string myquery = "select Book_Name,Author_Name,Publisher_Name,Book_Price,Published_Date from book_details where Book_Id='" + bookIdTxtBox_ToReturnBook.Text + "'";
                SqlCommand cmd = new SqlCommand(myquery, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                
                if (sdr.Read())  
                {
                    bookName_lbl.Content = "Book Name : " + sdr["Book_Name"].ToString();
                    authorName_lbl.Content = "Author Name : " + sdr["Author_Name"].ToString();
                    pubName_lbl.Content = "Publisher Name : " + sdr["Publisher_Name"].ToString();
                    bookPrice_lbl.Content = "Book Price : " + sdr["Book_Price"].ToString();
                    publishDate_lbl.Content = "Published Date : " + sdr["Published_Date"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                con.Close();
            }
        }

        public void returnBook()
        {
            try
            {
               
                int currQty = GetCurrentQuantity();
                string query = "SELECT * FROM issueBook WHERE user_id ="+ UserIdTxtBox_toReturnBook.Text+" AND book_id ="+ bookIdTxtBox_ToReturnBook.Text;
                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                command.Parameters.AddWithValue("@user_id", UserIdTxtBox_toReturnBook.Text);
                command.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToReturnBook.Text);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    reader.Close(); 
                    string deleteQuery = "DELETE FROM issueBook WHERE user_id =" + UserIdTxtBox_toReturnBook.Text + " AND book_id =" + bookIdTxtBox_ToReturnBook.Text;
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, con);
                    deleteCommand.Parameters.AddWithValue("@user_id", UserIdTxtBox_toReturnBook.Text);
                    deleteCommand.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToReturnBook.Text);
                    deleteCommand.ExecuteNonQuery();

                    string updateQuery = "UPDATE book_details SET Available_Quantity ="+ currQty +1+ " WHERE book_id =" + bookIdTxtBox_ToReturnBook.Text;
                    SqlCommand updateCommand = new SqlCommand(updateQuery, con);
                    updateCommand.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToReturnBook.Text);
                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Book returned successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No book to return","",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : "+ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public int GetCurrentQuantity()
        {
            int currentQuantity = -1; 

            try
            {
                 con.Open();
                 string query = "SELECT Available_Quantity FROM book_details WHERE book_id = @Book_Id";
                SqlCommand command = new SqlCommand(query, con);
                 command.Parameters.AddWithValue("@Book_Id", bookIdTxtBox_ToReturnBook.Text);
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    currentQuantity = Convert.ToInt32(result);
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while fetching current quantity: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
            return currentQuantity;
        }

        private void pay_btn_Click(object sender, RoutedEventArgs e)
        {
            paymentPage paymentPage = new paymentPage();
            paymentPage.Show();
        }
    }
}



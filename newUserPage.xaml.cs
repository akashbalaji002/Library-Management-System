using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Text.RegularExpressions;
using System.DirectoryServices;

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for newUserPage.xaml
    /// </summary>
    public partial class newUserPage : Window
    {
        public newUserPage()
        {
            InitializeComponent();
           
            
        }
        String pass_pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        static string s = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
        static SqlConnection con = new SqlConnection(s);


      
        public bool isInputValid()
        {
            if (!userId_txtBox.Text.All(char.IsDigit))
            {
                MessageBox.Show("User Id should be Numeric","failed",MessageBoxButton.OK,MessageBoxImage.Error);
                userId_txtBox.Clear();
                userId_txtBox.Focus();
                return false;
            }
            string ss = "[~!@#$%^&*()_+{}\\[\\]:;,.<>/?-]";
            if (!(fullname_txtBox.Text.All(char.IsDigit)) && Regex.IsMatch(fullname_txtBox.Text,ss)==true)   
            {
                MessageBox.Show("Enter a Valid Name", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                fullname_txtBox.Clear();
                fullname_txtBox.Focus();
                return false;
            }
            String phno_regex = "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$";
            if (Regex.IsMatch(phno_txtBox.Text,phno_regex)==false)
            {
                MessageBox.Show("Please enter a valid Phone Number", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                phno_txtBox.Clear();
                phno_txtBox.Focus();
                return false;
            }
            if (Regex.IsMatch(password_passwordBox.Password.ToString(), pass_pattern)==false)
            {
                MessageBox.Show("Please Provide a Strong Password");
                password_passwordBox.Clear();
                password_passwordBox.Focus();
                return false;

            }
            return true;
        }


        public bool isValid()
        {
            if(userId_txtBox.Text=="")
            {
                MessageBox.Show("User Id is required","failed",MessageBoxButton.OK,MessageBoxImage.Error);
                userId_txtBox.Focus();
                return false;
            }
            if (fullname_txtBox.Text == "")
            {
                MessageBox.Show("Name is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                fullname_txtBox.Focus();
                return false;
            }
            if (address_txtBox.Text == "")
            {
                MessageBox.Show("Address is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                address_txtBox.Focus();
                return false;
            }
            if (phno_txtBox.Text == "")
            {
                MessageBox.Show("Phone No is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                phno_txtBox.Focus();
                return false;
            }
            
            if (password_passwordBox.Password=="")
            {
                MessageBox.Show("Password is required", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                password_passwordBox.Focus();
                return false;
            }
            return true;
        }

        public bool checkUserId()
        {
            bool user_id_available = false;
            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "Select * from user_details where user_id='"+int.Parse(userId_txtBox.Text)+"'";
            SqlConnection con = new SqlConnection(mycon);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = myquery;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                user_id_available = true;
            }
            con.Close();
            return user_id_available;
        }

        public bool LoginSuccess()
        {
            bool loginSuccessful = false;
            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "Select * from user_details where user_id='" + int.Parse(userId_txtBox.Text) + "' and password = '" +password_passwordBox.Password;
            SqlDataAdapter da = new SqlDataAdapter(myquery, mycon); 

            DataTable dt = new DataTable();
            da.Fill(dt); 
            if(dt.Rows.Count > 0)
            {
                loginSuccessful = true; 
            } 
            return loginSuccessful;
        }

        private void register_btn_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                if (isValid() && isInputValid())
                {
                    if(checkUserId())
                    {
                        MessageBox.Show("User Id Already Exixst.Try with different one", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                        userId_txtBox.Clear();
                        userId_txtBox.Focus();
                    }
                    else
                    {
                        string query = "insert into user_details values(@user_id,@fullname,@addres,@ph_no,@user_pass)";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@user_id", userId_txtBox.Text);
                        cmd.Parameters.AddWithValue("@fullname", fullname_txtBox.Text);
                        cmd.Parameters.AddWithValue("@addres", address_txtBox.Text);
                        cmd.Parameters.AddWithValue("@ph_no", phno_txtBox.Text);
                        cmd.Parameters.AddWithValue("@user_pass", password_passwordBox.Password);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Registered Successfully", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                       
                    }
                   
                        this.Hide();
                        exUserLoginPage exUserLoginPage= new exUserLoginPage();
                        exUserLoginPage.Show();   
                }
            }
            catch(SqlException se) {
                MessageBox.Show(se.Message);
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            userPage obj = new userPage();
            obj.Show();
            this.Close();
        }
    }
}

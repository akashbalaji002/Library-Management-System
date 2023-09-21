using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    
    public partial class userUpdatePage : Window
    {
        public userUpdatePage()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        public bool isValid()
        {
            if(UpdUser_NameTxtBox.Text=="" || UpdAddress_TxtBox.Text=="" || UpdPhone_txtBox.Text=="" || UpdPass_PasswordBox.Password=="")
            {
                return false;
            }
            string ss = "[~!@#$%^&*()_+{}\\[\\]:;,.<>/?-]";
            if (!(UpdUser_NameTxtBox.Text.All(char.IsDigit)) && Regex.IsMatch(UpdUser_NameTxtBox.Text, ss) == true)
            {
                MessageBox.Show("Enter a Valid Name", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                UpdUser_NameTxtBox.Clear();
                UpdUser_NameTxtBox.Focus();
                return false;
            }
            String phno_regex = "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$";
            if (Regex.IsMatch(UpdPhone_txtBox.Text, phno_regex) == false)
            {
                MessageBox.Show("Please enter a valid Phone Number", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
                UpdPhone_txtBox.Clear();
                UpdPhone_txtBox.Focus();
                return false;
            }
            String pass_pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";
            if (Regex.IsMatch(UpdPass_PasswordBox.Password.ToString(), pass_pattern) == false)
            {
                MessageBox.Show("Please Provide a Strong Password", "failed", MessageBoxButton.OK, MessageBoxImage.Error);
               UpdPass_PasswordBox.Clear();
                UpdPass_PasswordBox.Focus();
                return false;

            }
            return true;

        }
        private void UpdateUser_btn_Click(object sender, RoutedEventArgs e)
        {
            if (isValid())
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update user_details set fullname='" + UpdUser_NameTxtBox.Text
                        + "',addres='" + UpdAddress_TxtBox.Text + "',ph_no='" + UpdPhone_txtBox.Text +
                        "',user_pass='" + UpdPass_PasswordBox.Password + "' where user_id=" + exUserLoginPage.temp, con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Details Updated Successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
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
            else
            {
                MessageBox.Show("Please Provide all details and valid","Failed",MessageBoxButton.OK, MessageBoxImage.Error);    
            }
           
        }
    }
}

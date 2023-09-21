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

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for exsistingUserLoginPage.xaml
    /// </summary>
    public partial class exsistingUserLoginPage : Window
    {
      
        public bool LoginSuccess()
        {
            bool loginSuccessful = false;
            string mycon = "Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2";
            string myquery = "Select * from user_details where user_id='" + int.Parse(LoginUserId_textBox.Text) + "' and password = '" + LoginPassword_passBox.Password;
            SqlDataAdapter da = new SqlDataAdapter(myquery, mycon);

            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                loginSuccessful = true;
            }
            return loginSuccessful;
        }

        private void LoginUserId_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

  
}

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
    /// <summary>
    /// Interaction logic for BookUpdatePage.xaml
    /// </summary>
    public partial class BookUpdatePage : Window
    {
        public BookUpdatePage()
        {
            InitializeComponent();
        }
          viewPageBooks view = new viewPageBooks();

        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        private void Update_btn_onUpdatePage_Click(object sender, RoutedEventArgs e)
        {
         
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("update book_details set Book_Name='" + UpdBood_NameTxtBox.Text
                    + "',Author_Name='" + UpdAuthor_NameTxtBox.Text + "',Publisher_Name='" + UpdPub_NameTxtBox.Text +
                    "',Number_Of_Pages='" + UpdNoOfPagesTxtBox.Text + "',Book_Price='" + UpdBook_PriceTxtBox.Text + "' where Book_Id="+ viewPageBooks.i, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Updated Successfully", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
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

    }
}

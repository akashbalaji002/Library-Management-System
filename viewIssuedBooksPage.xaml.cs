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
    /// Interaction logic for viewIssuedBooksPage.xaml
    /// </summary>
    public partial class viewIssuedBooksPage : Window
    {
        public viewIssuedBooksPage()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=IN11W-BALAJI-3;Initial Catalog=LmsDB;Persist Security Info=True;User ID=sa;Password=Akashbalaji@2");
        private void LoadIssuedBook_btn_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select i.user_id,i.Book_Id, i.Book_Name, i.issueDate, i.returnDate,b.Author_Name, b.Publisher_Name, b.Book_Price, b.Number_Of_Pages,b.Published_Date from issueBook as i join book_details as b on i.Book_Id=b.Book_Id;", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            con.Close();
            issuedBoodDetailsGridView.ItemsSource = dt.DefaultView;
        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

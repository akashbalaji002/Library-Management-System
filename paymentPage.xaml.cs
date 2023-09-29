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

namespace LMS_Project
{
    /// <summary>
    /// Interaction logic for paymentPage.xaml
    /// </summary>
    public partial class paymentPage : Window
    {
        public paymentPage()
        {
            InitializeComponent();
            penAmt_lbl.Content = returnBookPage.s;
        }

        public   bool isPaid()
        {

            string s = returnBookPage.penaltyAmount.ToString();
            if (s== payAmt_txtBox.Text)
            {
                MessageBox.Show("Payment Sucessful, Now you can return the book", "", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Please pay the due amount to return the book", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        public static string returnPenulty = "";
        private void pay_btn_Click(object sender, RoutedEventArgs e)
        {
            if(isPaid())
            {
                penAmt_lbl.Content = string.Empty;
                returnPenulty = penAmt_lbl.Content.ToString();
               
                this.Close();
            }
        }
        
    }
}


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

namespace h2SygehusnordLoc
{
    /// <summary>
    /// Interaction logic for pinVerify.xaml
    /// </summary>
    public partial class pinVerify : Window
    {
        public int pincode { get; private set; }

        public pinVerify()
        { 
            InitializeComponent();
        }

        public void verify(Window test)
        {
            this.Show();
            if(verifyPin())
                test.Show();
        }

        private bool verifyPin()
        {
            string pin = MainWindow.loggedInPin;
            if (pin == pwPincode.Password)
                return true;
            else
            { 
                MessageBox.Show("Forkert pinkode");
                return false;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void btnVerify_Click(object sender, RoutedEventArgs e)
        {
            verifyPin();
        }
    }
}
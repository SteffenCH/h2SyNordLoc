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
    /// Interaction logic for winDashboard.xaml
    /// </summary>
    public partial class winDashboard : Window
    {
        MainWindow main = new MainWindow();
        InformationBuilding ib = new InformationBuilding();
        DepartmentWindow dw = new DepartmentWindow();
        createAccount ca = new createAccount();

        public winDashboard()
        {
            InitializeComponent();
        }

        //Show Information Building window
        private void btnBuilding_Click(object sender, RoutedEventArgs e)
        {
            ib.ShowDialog();
        }

        //Show Department Window window
        private void btnDepartment_Click(object sender, RoutedEventArgs e)
        {
            dw.ShowDialog();
        }

        //Show Create Account window
        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            ca.ShowDialog();
        }

        //Close the dashboard and open the login screen
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            main.ShowDialog();
        }
    }
}

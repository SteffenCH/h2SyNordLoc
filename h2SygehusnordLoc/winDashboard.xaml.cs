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
        InformationBuilding infobuild = new InformationBuilding();
        DepartmentWindow departmentWindow = new DepartmentWindow();

        public winDashboard()
        {
            InitializeComponent();
        }

        private void btnBuilding_Click(object sender, RoutedEventArgs e)
        {
            infobuild.ShowDialog();
        }

        private void btnDepartment_Click(object sender, RoutedEventArgs e)
        {
            departmentWindow.ShowDialog();
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

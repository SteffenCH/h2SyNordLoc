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
    /// Interaction logic for CreateDepartment.xaml
    /// </summary>
    public partial class CreateDepartment : Window
    {
        private databaseContext db = new databaseContext();
        public event EventHandler closeEvent;

        public CreateDepartment()
        {
          
            InitializeComponent();



        }

        private void UpdateDB()
        {
            this.db.SaveChanges();

            if (this.closeEvent != null)
            {
                this.closeEvent(this, EventArgs.Empty);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            db.Department.Add(new Department
            {
                hall_ID = Convert.ToInt32(tbHallID.Text),
                building_ID = Convert.ToInt32(tbBuildingID.Text),
                department_name = tbDepartmentName.Text,
                created_at = Convert.ToDateTime(dpCreatedAt.Text)
            });

            UpdateDB();

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

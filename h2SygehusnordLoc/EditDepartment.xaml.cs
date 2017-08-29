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
    /// Interaction logic for EditDepartment.xaml
    /// </summary>
    public partial class EditDepartment : Window
    {
        private databaseContext db = new databaseContext();
        private Department department;
        public event EventHandler closeEvent;

        public EditDepartment(databaseContext db, Department department)
        {
            this.db = db;
            this.department = department;

            InitializeComponent();

            tbHallID.Text = department.hall_ID.ToString();
            tbBuildingID.Text = department.building_ID.ToString();
            tbDepartmentName.Text = department.department_name;
            dpCreatedAt.Text = department.created_at.ToString();


        }

        private void UpdateDB()
        {
            this.db.SaveChanges();

            if (this.closeEvent != null)
            {
                this.closeEvent(this, EventArgs.Empty);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            department.hall_ID = Convert.ToInt32(tbHallID.Text);
            department.building_ID = Convert.ToInt32(tbBuildingID.Text);
            department.department_name = tbDepartmentName.Text;
            department.created_at = Convert.ToDateTime(dpCreatedAt.Text);

            UpdateDB();

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}


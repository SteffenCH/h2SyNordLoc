using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        private databaseContext db = new databaseContext();
        private List<Department> departmentList = new List<Department>();

        public DepartmentWindow()
        {
            InitializeComponent();

            UpdateDataGrid(this, EventArgs.Empty);

            departmentList = db.Department.ToList();
            datagridDepartment.ItemsSource = departmentList;
        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {
            departmentList = new List<Department>();
            db = new databaseContext();

            departmentList = db.Department.ToList();

            if (tbSearchBox.Text != null && tbSearchBox.Text != "")
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                departmentList = departmentList.Where(c => culture.CompareInfo.IndexOf(c.department_name, tbSearchBox.Text, CompareOptions.IgnoreCase) >= 0).ToList();
            }

            datagridDepartment.ItemsSource = departmentList;
        }

        private void tbSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                UpdateDataGrid(this, EventArgs.Empty);

                CultureInfo culture = CultureInfo.CurrentCulture;
                departmentList = departmentList.Where(c => culture.CompareInfo.IndexOf(c.department_name, tbSearchBox.Text, CompareOptions.IgnoreCase) >= 0).ToList();

                datagridDepartment.ItemsSource = departmentList;
            }
        }

        private void datagridDepartment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Department department = ((FrameworkElement)e.OriginalSource).DataContext as Department;

            if (department != null)
            {
                EditDepartment editDepartment = new EditDepartment(db, department);
                editDepartment.closeEvent += new EventHandler(UpdateDataGrid);
                editDepartment.ShowDialog();
            }
        }

        private void btnCreateDepartment_Click(object sender, RoutedEventArgs e)
        {
            CreateDepartment createDepartment = new CreateDepartment();
            createDepartment.ShowDialog();

            UpdateDataGrid(this, EventArgs.Empty);
        }

        private void btnDeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Er du sikker på at du vil slette?", "Slet", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int ID = (datagridDepartment.SelectedItem as Department).ID;
                    Department department = (from Department in db.Department where Department.ID == ID select Department).SingleOrDefault();
                    db.Department.Remove(department);
                    db.SaveChanges();
                    datagridDepartment.ItemsSource = db.Department.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Data er slettet!", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
            tbSearchBox.Text = "";
            UpdateDataGrid(this, EventArgs.Empty);
        }
    }
}

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

namespace h2SygehusnordLoc.Windows
{
    /// <summary>
    /// Interaction logic for EditHall.xaml
    /// </summary>
    public partial class EditHall : Window
    {
        private Hall hall;
        databaseContext db = new databaseContext();
        public event EventHandler closeEvent;

        public EditHall(databaseContext db, Hall hall)
        {
            InitializeComponent();

            this.db = db;
            this.hall = hall;

            tbConsumption_ID.Text = hall.consumption_ID.ToString();
            tbDepartment_ID.Text = hall.department_ID.ToString();
            dpCreated_at.Text = hall.created_at.ToString();
        }

        private void Updatedb()
        {
            this.db.SaveChanges();

            if (this.closeEvent != null)
            {
                this.closeEvent(this, EventArgs.Empty);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hall.consumption_ID = Convert.ToInt32(tbConsumption_ID.Text);
                hall.department_ID = Convert.ToInt32(tbDepartment_ID.Text);
                hall.created_at = Convert.ToDateTime(dpCreated_at.Text);
            }
            catch
            {
                MessageBox.Show("Data er blevet opdateret!", "Opdateret", MessageBoxButton.OK, MessageBoxImage.Information);
                Updatedb();
                Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

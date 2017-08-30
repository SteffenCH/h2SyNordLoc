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
    /// Interaction logic for EditBuilding.xaml
    /// </summary>
    public partial class EditBuilding : Window
    {
        private Building building;
        databaseContext db = new databaseContext();
        public event EventHandler closeEvent;

        public EditBuilding(databaseContext db, Building building)
        {
            InitializeComponent();

            this.db = db;
            this.building = building;

            tbAddress.Text = building.address;
            tbCity.Text = building.city;
            tbZipcode.Text = building.zipcode;
            tbRoom_count.Text = building.room_count.ToString();
            dpCreated_at.Text = building.created_at.ToString();

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
                building.address = tbAddress.Text;
                building.city = tbCity.Text;
                building.zipcode = tbZipcode.Text;
                building.room_count = Convert.ToInt32(tbRoom_count.Text);
                building.created_at = Convert.ToDateTime(dpCreated_at.Text);
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

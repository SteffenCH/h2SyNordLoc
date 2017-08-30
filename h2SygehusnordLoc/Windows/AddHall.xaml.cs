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
    /// Interaction logic for AddHall.xaml
    /// </summary>
    public partial class AddHall : Window
    {
        public event EventHandler closeEvent;
        databaseContext db = new databaseContext();

        public AddHall()
        {
            InitializeComponent();
        }

        private void Updatedb()
        {
            this.db.SaveChanges();

            if (this.closeEvent != null)
            {
                this.closeEvent(this, EventArgs.Empty);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            db.Hall.Add(new Hall { ID = Convert.ToInt32(tbID.Text), consumption_ID = Convert.ToInt32(tbConsumption_ID.Text), department_ID = Convert.ToInt32(tbDepartment_ID.Text), created_at = Convert.ToDateTime(dpCreated_at.Text) });
            MessageBox.Show("Data er blevet opdateret!", "Opdateret", MessageBoxButton.OK, MessageBoxImage.Information);
            Updatedb();
            Close();
        }
    }
}

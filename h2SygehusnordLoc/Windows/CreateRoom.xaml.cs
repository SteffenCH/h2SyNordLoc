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
    /// Interaction logic for CreateRoom.xaml
    /// </summary>
    public partial class CreateRoom : Window
    {

        private databaseContext db = new databaseContext();
        public event EventHandler closeEvent;

        public CreateRoom()
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            db.Room.Add(new Room
            {
                hall_ID = Convert.ToInt32(tbHallID.Text),
                occupied = cbOccupied.IsChecked.Value,
                created_at = Convert.ToDateTime(dpCreatedAt.Text)
            });

            UpdateDB();

            Close();
        }
    }
}

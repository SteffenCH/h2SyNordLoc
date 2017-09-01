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
    /// Interaction logic for EditRoom.xaml
    /// </summary>
    public partial class EditRoom : Window
    {
        private databaseContext db = new databaseContext();
        private Room room;
        public event EventHandler closeEvent;

        public EditRoom(databaseContext db, Room room)
        {
            this.db = db;
            this.room = room;

            InitializeComponent();

            tbHallID.Text = room.hall_ID.ToString();
            cbOccupied.IsChecked = room.occupied;
            dpCreatedAt.Text = room.created_at.ToString();
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
            room.hall_ID = Convert.ToInt32(tbHallID.Text);
            room.occupied = cbOccupied.IsChecked.Value;
            room.created_at = Convert.ToDateTime(dpCreatedAt.Text);

            UpdateDB();

            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}

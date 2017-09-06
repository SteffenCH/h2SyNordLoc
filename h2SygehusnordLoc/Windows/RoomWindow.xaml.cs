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

namespace h2SygehusnordLoc.Windows
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window
    {
        private databaseContext db = new databaseContext();
        private List<Room> roomList = new List<Room>();

        public RoomWindow()
        {
            InitializeComponent();

            UpdateDataGrid(this, EventArgs.Empty);

            roomList = db.Room.ToList();
            datagridRoom.ItemsSource = roomList;

        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {
            roomList = new List<Room>();
            db = new databaseContext();

            roomList = db.Room.ToList();

            if (tbSearchBox.Text != null && tbSearchBox.Text != "")
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                roomList = roomList.Where(c => culture.CompareInfo.IndexOf(c.ID.ToString(), tbSearchBox.Text, CompareOptions.IgnoreCase) >= 0).ToList();
            }

            datagridRoom.ItemsSource = roomList;
        }

        private void btnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Er du sikker på at du vil slette?", "Slet", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int ID = (datagridRoom.SelectedItem as Room).ID;
                    Room room = (from Room in db.Room where Room.ID == ID select Room).SingleOrDefault();
                    db.Room.Remove(room);
                    db.SaveChanges();
                    datagridRoom.ItemsSource = db.Room.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Data er slettet!", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnCreateRoom_Click(object sender, RoutedEventArgs e)
        {
            CreateRoom createRoom = new CreateRoom();
            createRoom.ShowDialog();

            UpdateDataGrid(this, EventArgs.Empty);
        }

        private void tbSearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                UpdateDataGrid(this, EventArgs.Empty);

                CultureInfo culture = CultureInfo.CurrentCulture;
                roomList = roomList.Where(c => culture.CompareInfo.IndexOf(c.ID.ToString(), tbSearchBox.Text, CompareOptions.IgnoreCase) >= 0).ToList();

                datagridRoom.ItemsSource = roomList;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void datagridRoom_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Room room = ((FrameworkElement)e.OriginalSource).DataContext as Room;

            if (room != null)
            {
                EditRoom editRoom = new EditRoom(db, room);
                editRoom.closeEvent += new EventHandler(UpdateDataGrid);
                editRoom.ShowDialog();
            }
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

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
    /// Interaction logic for HallWindow.xaml
    /// </summary>
    public partial class HallWindow : Window
    {
        private databaseContext db = new databaseContext();
        List<Hall> hallList = new List<Hall>();

        public HallWindow()
        {
            InitializeComponent();

            UpdateDataGrid(this, EventArgs.Empty);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddBuilding addBuilding = new AddBuilding();
            addBuilding.ShowDialog();
            UpdateDataGrid(this, EventArgs.Empty);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                UpdateDataGrid(this, EventArgs.Empty);

                CultureInfo culture = CultureInfo.CurrentCulture;
                hallList = hallList.Where(c => culture.CompareInfo.IndexOf(c.ID.ToString(), tbSearch.Text, CompareOptions.IgnoreCase) >= 0).ToList();

                dataGridHall.ItemsSource = hallList;
            }
        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {
            hallList = new List<Hall>();
            db = new databaseContext();

            hallList = db.Hall.ToList();

            if (tbSearch.Text != null && tbSearch.Text == "")
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                hallList = hallList.Where(c => culture.CompareInfo.IndexOf(c.ID.ToString(), tbSearch.Text, CompareOptions.IgnoreCase) >= 0).ToList();
            }

            dataGridHall.ItemsSource = hallList;
        }

        private void dataGridBuilding_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Building building = ((FrameworkElement)e.OriginalSource).DataContext as Building;

            if (building != null)
            {

                EditBuilding editBuilding = new EditBuilding(db, building);
                editBuilding.closeEvent += new EventHandler(UpdateDataGrid);
                editBuilding.ShowDialog();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            e.Cancel = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Er du sikker på at du vil slette?", "Slet", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int ID = (dataGridHall.SelectedItem as Building).ID;
                    Hall hall = (from Hall in db.Hall where Hall.ID == ID select Hall).SingleOrDefault();
                    db.Hall.Remove(hall);
                    db.SaveChanges();
                    dataGridHall.ItemsSource = db.Hall.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Data er slettet!", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

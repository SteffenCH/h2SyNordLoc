using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for InformationBuilding.xaml
    /// </summary>
    public partial class InformationBuilding : Window
    {
        private databaseContext db = new databaseContext();
        List<Building> buildingList = new List<Building>();

        public InformationBuilding()
        {
            InitializeComponent();

            UpdateDataGrid(this, EventArgs.Empty);

            buildingList = db.Building.ToList();
            dataGridBuilding.ItemsSource = buildingList;
            /*var query = (from b in db.Building
            var query = (from b in db.Building

                         select new
                         {
                             b.ID,
                             b.address,
                             b.city,
                             b.zipcode,
                             b.room_count,
                             b.created_at
                         });
            dataGridBuilding.Items.Clear();
            foreach (var item in query)
            {
                dataGridBuilding.Items.Add(item);
            }*/
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddBuilding addBuilding = new AddBuilding();
            addBuilding.ShowDialog();
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                UpdateDataGrid(this, EventArgs.Empty);

                CultureInfo culture = CultureInfo.CurrentCulture;
                buildingList = buildingList.Where(c => culture.CompareInfo.IndexOf(c.address, tbSearch.Text, CompareOptions.IgnoreCase) >= 0 || culture.CompareInfo.IndexOf(c.city, tbSearch.Text, CompareOptions.IgnoreCase) >= 0 || culture.CompareInfo.IndexOf(c.zipcode, tbSearch.Text, CompareOptions.IgnoreCase) >= 0).ToList();
                
                dataGridBuilding.ItemsSource = buildingList;

                /*var searchValue = tbSearch.Text.Trim();
                var query = (from b in db.Building
                             where (b.address.StartsWith(searchValue))
                             select new
                             {
                                 b.ID,
                                 b.address,
                                 b.city,
                                 b.zipcode,
                                 b.room_count,
                                 b.created_at
                             });
                dataGridBuilding.Items.Clear();
                foreach (var item in query)
                {
                    dataGridBuilding.Items.Add(item);
                }*/
            }
        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {
            buildingList = new List<Building>();
            db = new databaseContext();

            buildingList = db.Building.ToList();

            if (tbSearch.Text != null && tbSearch.Text == "")
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                buildingList = buildingList.Where(c => culture.CompareInfo.IndexOf(c.address, tbSearch.Text, CompareOptions.IgnoreCase) >= 0).ToList();
            }

            dataGridBuilding.ItemsSource = buildingList;
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
            /*int ID = (dataGridBuilding.SelectedItem as Building).ID;
            Building building = (from Building in db.Building where Building.ID == building select Building)*/

            /*int medlemID = (dataGrid.SelectedItem as Medlem).MedlemID;
            Medlem medlem = (from Medlems in db.Medlemmer where Medlems.MedlemID == medlemID select Medlems).SingleOrDefault();
            db.Medlemmer.Remove(medlem);
            db.SaveChanges();
            dataGrid.ItemsSource = db.Medlemmer.ToList();

            this.Hide();
            MainWindow main = new MainWindow();
            main.ShowDialog();*/

        }
    }
}

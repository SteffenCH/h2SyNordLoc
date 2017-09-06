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
        //public event EventHandler closeEvent;
        private databaseContext db = new databaseContext();
        List<Building> buildingList = new List<Building>();

        public InformationBuilding()
        {
            InitializeComponent();

            UpdateDataGrid(this, EventArgs.Empty);

            /*var query = from b in buildingList
                        orderby b.address ascending
                        select b;*/

            //dataGridBuilding.ItemsSource = buildingList.ToList();

            /*buildingList = db.Building.ToList();
            dataGridBuilding.ItemsSource = buildingList;*/
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

        /*private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddBuilding addBuilding = new AddBuilding();
            addBuilding.ShowDialog();
            UpdateDataGrid(this, EventArgs.Empty);
        }*/

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
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Er du sikker på at du vil slette?", "Slet", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    int ID = (dataGridBuilding.SelectedItem as Building).ID;
                    Building building = (from Building in db.Building where Building.ID == ID select Building).SingleOrDefault();
                    db.Building.Remove(building);
                    db.SaveChanges();
                    dataGridBuilding.ItemsSource = db.Building.ToList();
                }    
            }
            catch
            {
                MessageBox.Show("Data er slettet!", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Updatedb()
        {
            this.db.SaveChanges();

            /*if (this.closeEvent != null)
            {
                this.closeEvent(this, EventArgs.Empty);
            }*/
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            db.Building.Add(new Building { address = tbAddress.Text, city = tbCity.Text, zipcode = tbZipcode.Text, room_count = Convert.ToInt32(tbRoom_count.Text), created_at = Convert.ToDateTime(dpCreated_at.Text) });
            MessageBox.Show("Data er blevet opdateret!", "Opdateret", MessageBoxButton.OK, MessageBoxImage.Information);
            Updatedb();
            tbAddress.Text = null;
            tbCity.Text = null;
            tbZipcode.Text = null;
            tbRoom_count.Text = null;
            dpCreated_at.Text = null;
            dataGridBuilding.ItemsSource = buildingList;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            tbAddress.Text = null;
            tbCity.Text = null;
            tbZipcode.Text = null;
            tbRoom_count.Text = null;
            dpCreated_at.Text = null;
        }
    }
}

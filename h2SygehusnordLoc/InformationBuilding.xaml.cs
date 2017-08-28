using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Building building;
        private databaseContext db = new databaseContext();
        List<Building> buildingList = new List<Building>();
        public ObservableCollection<Building> Buildings { get; set; }

        public InformationBuilding()
        {
            InitializeComponent();

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
            }
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var searchValue = tbSearch.Text.Trim();
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
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                object item = dataGridBuilding.SelectedItem;
                databaseContext db = new databaseContext();

                int m = int.Parse((dataGridBuilding.SelectedCells[0].Column.GetCellContent(item) as
                    TextBox).Text);
            }*/
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            tbAddress.Text = null;
            tbCity.Text = null;
            tbZipCode.Text = null;
            tbRoomCount.Text = null;
            dataGridBuilding.ItemsSource = null;
            dataGridBuilding.Items.Clear();
        }

        private void dataGridBuilding_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dg = (DataGrid)sender;
            
            if (dataGridBuilding.SelectedIndex >= 0)
            {
                
                //tbAddress.SelectedText = building.address;
            }
        }
    }
}

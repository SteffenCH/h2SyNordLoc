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
    /// Interaction logic for Oplysninger.xaml
    /// </summary>
    public partial class Oplysninger : Window
    {
        private databaseContext db = new databaseContext();
        List<Building> buildingList = new List<Building>();
        public ObservableCollection<Building> Buildings { get; set; }

        public Oplysninger()
        {
            InitializeComponent();

            /*buildingList = db.Building.ToList();
            dataGridBuilding.ItemsSource = buildingList;*/

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
            //    var searchValue = tbSearch.Text.Trim();
            //    var query = (from b in db.Building
            //                 join c in db.Consumption on b.ID equals c.ID
            //                 join g in db.Department on c.ID equals g.ID into dt
            //                 from x in dt.DefaultIfEmpty()
            //                 where (b.address.StartsWith(searchValue) ||
            //                 b.city.Contains(searchValue) ||
            //                 b.zipcode.Contains(searchValue))
            //                 orderby b.address ascending

            //                 /* Skal i dto */
            //                 select new
            //                 {
            //                     b.ID,
            //                     b.address,
            //                     b.city,
            //                     b.zipcode,
            //                     b.room_count,
            //                     b.created_at
            //                 });

            //    dataGridBuilding.Items.Clear();
            //    /* Til at få data ud af query */
            //    foreach (var item in query)
            //    {
            //        dataGridBuilding.Items.Add(item);
            //    }
            }
        }
    }
}

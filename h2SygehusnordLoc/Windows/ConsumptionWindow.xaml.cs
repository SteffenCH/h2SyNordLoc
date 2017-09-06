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
    /// Interaction logic for ConsumptionWindow.xaml
    /// </summary>
    public partial class ConsumptionWindow : Window
    {
        private databaseContext db = new databaseContext();
        List<Consumption> consumptionList = new List<Consumption>();

        public ConsumptionWindow()
        {
            InitializeComponent();

            UpdateDataGrid(this, EventArgs.Empty);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                UpdateDataGrid(this, EventArgs.Empty);

                CultureInfo culture = CultureInfo.CurrentCulture;
                consumptionList = consumptionList.Where(c => culture.CompareInfo.IndexOf(c.ID.ToString(), tbSearch.Text, CompareOptions.IgnoreCase) >= 0).ToList();

                dataGridConsumption.ItemsSource = consumptionList;
            }
        }

        private void UpdateDataGrid(object sender, EventArgs e)
        {
            consumptionList = new List<Consumption>();
            db = new databaseContext();

            consumptionList = db.Consumption.ToList();

            if (tbSearch.Text != null && tbSearch.Text == "")
            {
                CultureInfo culture = CultureInfo.CurrentCulture;

                consumptionList = consumptionList.Where(c => culture.CompareInfo.IndexOf(c.ID.ToString(), tbSearch.Text, CompareOptions.IgnoreCase) >= 0).ToList();
            }

            dataGridConsumption.ItemsSource = consumptionList;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            /*db.Consumption.Add(new Consumption { ID = Convert.ToInt32(tbID.Text), consumption_ID = Convert.ToInt32(tbConsumption_ID.Text), department_ID = Convert.ToInt32(tbDepartment_ID.Text), created_at = Convert.ToDateTime(dpCreated_at.Text) });
            MessageBox.Show("Data er blevet opdateret!", "Opdateret", MessageBoxButton.OK, MessageBoxImage.Information);
            Updatedb();
            Close();*/
        }
    }
}

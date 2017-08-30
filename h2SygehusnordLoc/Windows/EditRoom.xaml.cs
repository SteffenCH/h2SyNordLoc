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
        }
    }
}

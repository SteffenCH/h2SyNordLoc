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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace h2SygehusnordLoc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string LoggedInUsername { get; private set; }
        public static int LoggedInId { get; private set; }
        public static bool LoggedIn { get; private set; }
        public databaseContext db = new databaseContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sanitizeUserInput()
        {
            string username = tbUsername.Text;
            string password = pwPassword.Password;
            if (username.Length < 8 || password.Length < 8)
            {
                MessageBox.Show("Username or password was under 8 char's, please enter a valid login");
            }
            else
            {
                validateUserLogin(username, password);

            }
        }

        private void validateUserLogin(string username, string password)
        {
            var user = db.Login.Where(o => o.username == username ).ToArray();
            try { 
            if (BCrypt.Net.BCrypt.Verify(password, user[0].password_hash)) {
                 LoggedIn = true;
                 login(user[0].ID,user[0].username);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Account not found");
                LoggedIn = false;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void login(int userId,string username)
        {
            if (LoggedIn) {
            LoggedInId = userId;
            winDashboard dashborard = new winDashboard();
            dashborard.Show();
            this.Close();
            }
        }

        public static void logoutUser()
        {
            LoggedIn = false;
            LoggedInId = 0;
            LoggedInUsername = "";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            sanitizeUserInput();
        }
    }
}
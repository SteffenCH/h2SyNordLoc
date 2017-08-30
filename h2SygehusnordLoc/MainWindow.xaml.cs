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
        public static string loggedInUsername { get; private set; }
        public static string loggedInPin { get; private set; }
        public static int loggedInId { get; private set; }
        public static bool loggedIn { get; private set; }
        public databaseContext db = new databaseContext();
        public static pinVerify pinVerify = new pinVerify();
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
               MessageBox.Show("Brugernavn og/eller adgangskode er under 8 karaktere");
           }
           else
           {
               validateUserLogin(username, password);           }
        }

        private void validateUserLogin(string username, string password)
        {
            var user = db.Login.Where(o => o.username == username ).ToArray();
            try { 
            if (BCrypt.Net.BCrypt.Verify(password, user[0].password_hash)) {
                 loggedIn = true;
                 login(user[0].ID,user[0].username,user[0].pin);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kontoen blev ikke fundet");
                loggedIn = false;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void login(int userId,string username, string pin)
        {
            if (LoggedIn) {
            LoggedInId = userId;
            winDashboard dashboard = new winDashboard();
            dashboard.Show();
            this.Hide();
            }
        }

        public static void logoutUser()
        {
            loggedIn = false;
            loggedInId = 0;
            loggedInUsername = "";
            loggedInPin = "";
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            sanitizeUserInput();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
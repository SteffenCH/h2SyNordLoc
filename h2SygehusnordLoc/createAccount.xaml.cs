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
using BCrypt.Net;
namespace h2SygehusnordLoc
{
    /// <summary>
    /// Interaction logic for createAccount.xaml
    /// </summary>
    public partial class createAccount : Window
    {
        public databaseContext db = new databaseContext();
        public createAccount()
        {
            InitializeComponent();
        }

        private void sanitizeUserInput()
        {
            string username = tbUsername.Text;
            string password = pwPassword.Password;
            string passwordVerify = pwPasswordverify.Password;
            if (username.Length < 8 || password.Length < 8 && password == passwordVerify)
            {
                MessageBox.Show("Username or password was under 8 char's, please enter a valid login");
            }
            else
            {
                string passHash = BCrypt.Net.BCrypt.HashPassword(password);
                createUser(username, passHash);
            }
        }

        private void createUser(string username,string password_hash)
        {
            db.Login.Add(new Login { username = username, password_hash = password_hash });
            try
            {
                db.SaveChanges();
                MessageBox.Show("Kontoen blev korrekt oprettet.");
            }
            catch (Exception e) {
                MessageBox.Show("Der er sket en fejl, vis denne besked til en fagidiot " + e);
            }
        }
        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            sanitizeUserInput();
            MessageBox.Show("");
        }
    }
}

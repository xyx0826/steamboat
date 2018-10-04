using steamboat.components;
using System.Text.RegularExpressions;
using System.Windows;

namespace steamboat.WPF
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (TestString(tb_username.Text) && passwordBox.SecurePassword != null)
            {
                SteamAccount Account = new SteamAccount(tb_username.Text, passwordBox.SecurePassword);
                ((MainWindow)Application.Current.MainWindow).NewAccount(Account);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid username and password.");
            }
        }

        /// <summary>
        /// Only allow alphanumerics and spaces.
        /// </summary>
        /// <param name="str">String to be tested.</param>
        /// <returns>Validity of specified string.</returns>
        private bool TestString(string str)
        {
            return Regex.IsMatch(str, @"^[A-Za-z0-9\s@]*$");
        }
    }
}

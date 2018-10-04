using steamboat.components;
using System.Windows;

namespace steamboat.WPF
{
    /// <summary>
    /// Interaction logic for EditAccount.xaml
    /// </summary>
    public partial class EditAccount : Window
    {
        const string _fakePassword = "12345678";

        public EditAccount()
        {
            InitializeComponent();
            MainWindow MW = ((MainWindow)Application.Current.MainWindow);
            tb_username.Text = MW.AccountList[MW.Listbox_Accounts.SelectedIndex].Alias;
            passwordBox.Password = _fakePassword;
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MW = ((MainWindow)Application.Current.MainWindow);
            MW.UpdateAccount(new SteamAccount(tb_username.Text, passwordBox.SecurePassword));
            MW.AccountList[MW.Listbox_Accounts.SelectedIndex].Alias = tb_name.Text;
            MW.AccountList[MW.Listbox_Accounts.SelectedIndex].SecurePassword = passwordBox.SecurePassword;
            MW.Listbox_Accounts.ItemsSource[MW.Listbox_Accounts.SelectedIndex] = tb_name.Text;
            this.Close();
        }
    }
}

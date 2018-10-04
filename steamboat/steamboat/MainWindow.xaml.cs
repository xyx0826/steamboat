using steamboat.components;
using steamboat.Utils;
using steamboat.WPF;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace steamboat
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		Steam steam = new Steam();
		public List<SteamAccount> AccountList;
        Database _db;

		public MainWindow()
		{
			InitializeComponent();
			CheckSteam();
			steam.CheckPath();

            _db = new Database();
            AccountList = _db.GetAllAccounts();
            Listbox_Accounts.ItemsSource = AccountList;
            Listbox_Accounts.DisplayMemberPath = "Alias";

        }

		private void button_KillSteam_Click(object sender, RoutedEventArgs e)
		{
			if (steam.IsRunning())
				steam.Kill();
			Thread.Sleep(200);
			CheckSteam();
		}

		private void CheckSteam()
		{
			if (steam.IsRunning())
			{
				Label_SteamStatus.Foreground = Brushes.ForestGreen;
				Label_SteamStatus.Content = "running";
			}
			else
			{
				Label_SteamStatus.Foreground = Brushes.Red;
				Label_SteamStatus.Content = "offline";
			}
		}

		public void NewAccount(SteamAccount account)
		{
            if (_db.AddAccount(account))
            {
                AccountList.Add(account);
                Listbox_Accounts.Items.Add(account.Alias);
            }
            // else: NewAccount failed because same-name account already exists
		}

        public void UpdateAccount(SteamAccount account)
        {
            var isSuccessful = _db.UpdateAccount(account);
        }

		private void ListBox_NewAccount(object sender, RoutedEventArgs e)
		{
			AddAccount AddWin = new AddAccount();
			AddWin.Owner = this;
			AddWin.Show();
		}

		private void Listbox_Accounts_MouseDoubleClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Switching accounts not yet implemented.");
		}

		private void Listbox_Accounts_Delete(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Are you sure?", 
				string.Format("Delete {0}?", AccountList[Listbox_Accounts.SelectedIndex].Alias),
				MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				AccountList.RemoveAt(Listbox_Accounts.SelectedIndex);
				Listbox_Accounts.Items.RemoveAt(Listbox_Accounts.SelectedIndex);
			}
		}

		private void Listbox_Accounts_Edit(object sender, RoutedEventArgs e)
		{
			EditAccount EditWin = new EditAccount();
			EditWin.Owner = this;
			EditWin.Show();
		}
	}
}

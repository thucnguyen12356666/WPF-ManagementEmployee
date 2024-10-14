using BookBussinessObject;
using BookRespository;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace BookManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IAccountRespository IAccountRespository;
        public MainWindow()
        {
            InitializeComponent();
            IAccountRespository = new AccountRespository();
            // Đọc thông tin từ Registry nếu tồn tại
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\YourAppName");
            if (key != null)
            {
                txtEmail.Text = key.GetValue("Email")?.ToString() ?? string.Empty;
                txtPassword.Password = key.GetValue("Password")?.ToString() ?? string.Empty;
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Account account = IAccountRespository.GetAccount(txtEmail.Text,txtPassword.Password);
            if (account != null && account.RoleId == 1)
            {

                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\YourAppName");
                key.SetValue("Email", txtEmail.Text);
                key.SetValue("Password", txtPassword.Password);
                key.Close();
                this.Hide();
                ManagementBookWindow managementBookWindow = new ManagementBookWindow();
                managementBookWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("You are not permission !");
            }

        }
    }
}
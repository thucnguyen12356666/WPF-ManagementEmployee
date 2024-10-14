using BookBussinessObject;
using BookRespository;
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

namespace BookManagement
{
    /// <summary>
    /// Interaction logic for AccountUserWindow.xaml
    /// </summary>
    public partial class AccountUserWindow : Window
    {
        private IAccountRespository accountRepository;
        public AccountUserWindow()
        {
            InitializeComponent();
            accountRepository = new AccountRespository();
            LoadAccounts();

        }
        private void LoadAccounts()
        {
            try
            {
                var book = accountRepository.GetAllAccounts();
                AccountsDataGrid.ItemsSource = book;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void AccountsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AccountsDataGrid.SelectedItem is Account selectedAccount)
            {
                // Cập nhật các ô nhập liệu với thông tin của cuốn sách đã chọn

                txtUsername.Text = selectedAccount.Username; // Cập nhật Username
                txtPassword.Password = selectedAccount.Password; // Cập nhật Password
                txtRoleId.Text = selectedAccount.RoleId.ToString(); // Cập nhật RoleId
            }
        }

        private void LoadAccountButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AccountsDataGrid.SelectedItem is Account selectedAccount) 
                {
                    selectedAccount.Username = txtUsername.Text;
                    selectedAccount.Password = txtPassword.Password;
                    selectedAccount.RoleId = Int32.Parse(txtRoleId.Text);
                    accountRepository.deleteAccount(selectedAccount.AccountId);
                    MessageBox.Show("Account deleted successfully."); 
                    LoadAccounts(); 
                }
                else
                {
                    MessageBox.Show("Please select an account to delete."); // Thông báo nếu không có tài khoản nào được chọn
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}"); // Thông báo lỗi
            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AccountsDataGrid.SelectedItem is Account selectedAccount) 
                {
                    selectedAccount.Username = txtUsername.Text; 
                    selectedAccount.Password = txtPassword.Password; 
                    selectedAccount.RoleId = Int32.Parse(txtRoleId.Text);

                    
                    accountRepository.updateAccount(selectedAccount);
                    MessageBox.Show("Account updated successfully."); 
                    LoadAccounts(); 
                }
                else
                {
                    MessageBox.Show("Please select an account to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}"); 
            }

        }
    }
}

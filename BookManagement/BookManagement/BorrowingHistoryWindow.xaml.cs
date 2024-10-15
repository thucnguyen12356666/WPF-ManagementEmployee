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
using static BookManagement.MainWindow;

namespace BookManagement
{
    /// <summary>
    /// Interaction logic for BorrowingHistoryWindow.xaml
    /// </summary>
    public partial class BorrowingHistoryWindow : Window
    {
        private IBorrowingHisRespository borrowingHistoryRepository;
        private IBooksRespository iBookRespository;
        private int currentAccountId;
        public BorrowingHistoryWindow()
        {
            InitializeComponent();
            borrowingHistoryRepository = new BorrowingHisRespository();
            iBookRespository = new BooksRespository();
            currentAccountId = AppState.AccountId;
            LoadBorrowingHistory();
        }
        private void LoadBorrowingHistory()
        {
            try
            {
                var borrowingHistory = borrowingHistoryRepository.GetBorrowingHisByAccountID(currentAccountId);
                BorrowingHistoryDataGrid.ItemsSource = borrowingHistory;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private void BorrowingHistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

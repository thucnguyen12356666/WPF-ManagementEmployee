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
using static BookManagement.MainWindow;

namespace BookManagement
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private IBorrowingHisRespository borrowingHistoryRepository;
        private IBooksRespository iBookRespository;
        private IAccountRespository iAccountRespository;
        private int currentAccountId;
        public UserWindow()
        {
            InitializeComponent();
            borrowingHistoryRepository = new BorrowingHisRespository();
            iBookRespository = new BooksRespository();
            iAccountRespository = new AccountRespository();
            currentAccountId = AppState.AccountId;
            LoadBook();
        }

        private void LoadBook()
        {
            try
            {
                 var availableBooks = iBookRespository.GetAvailableBooksForAccount(currentAccountId);
                BooksDataGrid.ItemsSource = availableBooks;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            BorrowingHistoryWindow borrowingHistoryWindow = new BorrowingHistoryWindow();
            borrowingHistoryWindow.ShowDialog();


        }
        private void BorrowButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                if (BooksDataGrid.SelectedItem is Book selectedBook)
                {
                    
                    BorrowingHistory borrowingHistory = new BorrowingHistory
                    {
                        BookId = selectedBook.BookId,
                        AccountId = currentAccountId,
                        BorrowDate = DateTime.Now,
                       
                    };

                    borrowingHistoryRepository.AddBorrowingHis(borrowingHistory);

                    
                    MessageBox.Show($"Bạn đã mượn sách: {selectedBook.Title}");

                   
                    var books = (List<Book>)BooksDataGrid.ItemsSource;
                    books.Remove(selectedBook);
                    BooksDataGrid.ItemsSource = null;
                    BooksDataGrid.ItemsSource = books;
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sách để mượn.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
    }
}

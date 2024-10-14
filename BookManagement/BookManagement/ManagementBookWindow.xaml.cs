using BookRespository;
using BookBussinessObject;
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
    /// Interaction logic for ManagementBookWindow.xaml
    /// </summary>
    public partial class ManagementBookWindow : Window
    {
        private IBooksRespository iBookRespository;
        public ManagementBookWindow()
        {
            InitializeComponent();
            iBookRespository = new BooksRespository();
        }

        private void BooksDataGrid_SelectionChanged()
        {
            try
            {
                var book = iBookRespository.GetAllBook();
                BooksDataGrid.ItemsSource = book;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BooksDataGrid_SelectionChanged();
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Book book = new Book()
                {
                    Title = txtTitle.Text,
                    Author = txtAuthor.Text,
                    Publisher = txtPublisher.Text,
                    YearPublished = Int32.Parse(txtYearPublished.Text),
                    Quantity = Int32.Parse(txtQuantity.Text),

                };
                iBookRespository.AddBook(book);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                BooksDataGrid_SelectionChanged();
            }


        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksDataGrid.SelectedItem is Book selectedBook) // Lấy cuốn sách đã chọn
                {
                    selectedBook.Title = txtTitle.Text;
                    selectedBook.Author = txtAuthor.Text;
                    selectedBook.Publisher = txtPublisher.Text;
                    selectedBook.YearPublished = Int32.Parse(txtYearPublished.Text);
                    selectedBook.Quantity = Int32.Parse(txtQuantity.Text);

                    iBookRespository.UpdateBook(selectedBook); // Cập nhật cuốn sách
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                BooksDataGrid_SelectionChanged();
                ClearInputFields();
            }


        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BooksDataGrid.SelectedItem is Book selectedBook) 
                {
                    selectedBook.Title = txtTitle.Text;
                    selectedBook.Author = txtAuthor.Text;
                    selectedBook.Publisher = txtPublisher.Text;
                    selectedBook.YearPublished = Int32.Parse(txtYearPublished.Text);
                    selectedBook.Quantity = Int32.Parse(txtQuantity.Text);

                    iBookRespository.DeleteBook(selectedBook.BookId); 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                BooksDataGrid_SelectionChanged();
                ClearInputFields();
            }


        }

        private void ManageAccountsButton_Click(object sender, RoutedEventArgs e)
        {
            AccountUserWindow accountUserWindow = new AccountUserWindow();
            accountUserWindow.ShowDialog();

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchTerm = SearchTextBox.Text.ToLower();
            var allBooks = iBookRespository.GetAllBook();

            var filteredBooks = allBooks.Where(b =>
                b.Title.ToLower().Contains(searchTerm) ||
                b.Author.ToLower().Contains(searchTerm) ||
                b.Publisher.ToLower().Contains(searchTerm)).ToList();

            BooksDataGrid.ItemsSource = filteredBooks;

        }

        private void BooksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra xem có cuốn sách nào được chọn không
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                
                txtTitle.Text = selectedBook.Title;
                txtAuthor.Text = selectedBook.Author;
                txtPublisher.Text = selectedBook.Publisher;
                txtYearPublished.Text = selectedBook.YearPublished.ToString();
                txtQuantity.Text = selectedBook.Quantity.ToString();
            }

        }
        private void ClearInputFields()
        {
            txtTitle.Text = string.Empty;
            txtAuthor.Text = string.Empty;
            txtPublisher.Text = string.Empty;
            txtYearPublished.Text = string.Empty;
            txtQuantity.Text = string.Empty;
        }
    }
}

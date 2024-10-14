using BookBussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDAO
{
    public class BooksDAO
    {
        private  LibraryContext _context;
        private static BooksDAO _instance = null;
        public static BooksDAO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BooksDAO();
                }
                return _instance;
            }
        }
        private BooksDAO()
        {
            _context = new LibraryContext();
        }
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
        public Book GetBookById(int id)
        {
            return _context.Books.SingleOrDefault(b => b.BookId == id);
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public void UpdateBook(Book book)
        {
            var bookToUpdate = _context.Books.SingleOrDefault(b => b.BookId == book.BookId);
            if (bookToUpdate != null)
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Author = book.Author;
                bookToUpdate.Publisher = book.Publisher;
                bookToUpdate.YearPublished = book.YearPublished;
                bookToUpdate.Quantity = book.Quantity;
                _context.SaveChanges();
            }
        }
        public void DeleteBook(int id)
        {
            var bookToDelete = _context.Books.SingleOrDefault(b => b.BookId == id);
            if (bookToDelete != null)
            {
                _context.Books.Remove(bookToDelete);
                _context.SaveChanges();
            }
        }


    }
}

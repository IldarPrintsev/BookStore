using BookStore.DAL.Models;
using System.Collections.Generic;

namespace BookStore.BLL.Interfaces
{
    public interface IBookManager
    {
        IEnumerable<Book> GetBooks();

        void MarkPurchasedBooks(string userEmail, IEnumerable<Book> books);

        void Subscribe(string email, int bookId);

        void Unsubscribe(string email, int bookId);

        void AddBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(int id);
    }
}

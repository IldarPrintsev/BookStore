using BookStore.DAL.Models;
using System.Collections.Generic;

namespace BookStore.DAL.Interfaces
{
    public interface IUserRepository
    {
        User Get(string email, string password);

        IEnumerable<Book> GetPurchasedBooks(string userEmail);

        bool Add(User user);

        void AddBook(string email, int id);

        void RemoveBook(string email, int bookId);
    }
}

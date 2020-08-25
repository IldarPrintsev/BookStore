using BookStore.DAL.Interfaces;
using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _db;

        public UserRepository(ApplicationContext db)
        {
            this._db = db;
        }

        public User Get(string email, string password)
        {
            var user = this._db.Users
                .FirstOrDefault(u => (u.Email == email) &&
                                    (u.Password == password));

            return user;
        }

        public IEnumerable<Book> GetPurchasedBooks(string userEmail)
        {
            var user = _db.Users.Include(e => e.Books).ThenInclude(e => e.Book).First(user => user.Email == userEmail);
            var books = user.Books.Select(ub => ub.Book);

            return books;
        }

        public bool Add(User user)
        {
            if(this._db.Users.FirstOrDefault(u => u.Email == user.Email) != null)
            {
                return false;
            }

            this._db.Users.Add(user);
            this._db.SaveChanges();

            return true;
        }

        public void AddBook(string email, int bookId)
        {
            var user = this._db.Users.FirstOrDefault(u => u.Email == email);
            user.Books.Add(new UserBook { BookId = bookId, UserId = user.Id });

            this._db.SaveChanges();
        }

        public void RemoveBook(string email, int bookId)
        {
            var user = this._db.Users.FirstOrDefault(u => u.Email == email);
            var userBook = this._db.UsersBooks.First(row => row.UserId == user.Id && row.BookId == bookId);

            _db.Remove(userBook);
            _db.SaveChanges();
        }
    }
}

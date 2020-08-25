using BookStore.DAL.Interfaces;
using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationContext _db;

        public BookRepository(ApplicationContext db)
        {
            this._db = db;
        }

        public IEnumerable<Book> GetAll()
        {
            IEnumerable<Book> books = this._db.Books;

            return books;
        }

        public void Add(Book book)
        {
            this._db.Books.Add(book);
            this._db.SaveChanges();
        }

        public void Update(Book book)
        {
            this._db.Books.Update(book);
            this._db.SaveChanges();
        }

        public void Delete(int id)
        {
            Book book = this._db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                throw new InvalidOperationException("Book is not found");
            }

            this._db.Books.Remove(book);
            this._db.SaveChanges();
        }
    }
}

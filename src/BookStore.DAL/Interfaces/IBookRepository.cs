using BookStore.DAL.Models;
using System.Collections.Generic;

namespace BookStore.DAL.Interfaces
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();

        void Add(Book book);

        void Update(Book book);

        void Delete(int id);
    }
}

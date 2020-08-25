using BookStore.BLL.Interfaces;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.BLL.Managers
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;

        public BookManager(IBookRepository bookRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Book> GetBooks()
        {
            try
            {
                return this._bookRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurs during getting the books: " + ex.Message, ex);
            }
        }

        public void MarkPurchasedBooks(string userEmail, IEnumerable<Book> books)
        {
            try
            {
                var purchasedBooks = this._userRepository.GetPurchasedBooks(userEmail);
                var markedBooks = books.Intersect(purchasedBooks);
                foreach(var markedBook in markedBooks)
                {
                    markedBook.IsPurchased = true;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurs during getting the purchased books: " + ex.Message, ex);
            }
        }

        public void Subscribe(string email, int bookId)
        {
            try
            {
                this._userRepository.AddBook(email, bookId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurs during subscribing to a book: " + ex.Message, ex);
            }
        }

        public void Unsubscribe(string email, int bookId)
        {
            try
            {
                this._userRepository.RemoveBook(email, bookId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurs during unsubscribing from a book: " + ex.Message, ex);
            }
        }

        public void AddBook(Book book)
        {
            try
            {
                this._bookRepository.Add(book);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An invalid operation during adding a book: " + ex.Message, ex);
            }
        }

        public void UpdateBook(Book book)
        {
            try
            {
                this._bookRepository.Update(book);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An invalid operation during updating a book: " + ex.Message, ex);
            }
        }

        public void DeleteBook(int id)
        {
            try
            {
                this._bookRepository.Delete(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An invalid operation during deleting a book: " + ex.Message, ex);
            }
        }
    }
}

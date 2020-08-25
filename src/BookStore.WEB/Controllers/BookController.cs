using BookStore.BLL.Interfaces;
using BookStore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BookStore.WEB.Controllers
{
    [ApiController]
    [Route("api/bookstore")]
    public class BookController : ControllerBase
    {
        private readonly IBookManager _bookManager;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookManager bookManager, ILogger<BookController> logger)
        {
            this._bookManager = bookManager;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                var books = this._bookManager.GetBooks();

                if (base.User.Identity.IsAuthenticated)
                {
                    var email = base.User.Identity.Name;
                    _bookManager.MarkPurchasedBooks(email, books);
                }

                return Ok(books);
            }
            catch (InvalidOperationException ex)
            {
                string message = "Programm error: " + ex.Message;
                this._logger.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = message });
            }
            catch (Exception ex)
            {
                string message = "Unknown error: " + ex.Message;
                this._logger.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = message });
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateSubscription(Book book)
        {
            try
            {
                var email = base.User.Identity.Name;
                if(book.IsPurchased)
                {
                    this._bookManager.Unsubscribe(email, book.Id);
                }
                else
                {
                    this._bookManager.Subscribe(email, book.Id);
                }

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                string message = "Programm error: " + ex.Message;
                this._logger.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = message });
            }
            catch (Exception ex)
            {
                string message = "Unknown error: " + ex.Message;
                this._logger.LogError(message);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = message });
            }
        }
    }
}

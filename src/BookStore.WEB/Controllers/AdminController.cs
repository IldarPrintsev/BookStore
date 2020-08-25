using BookStore.BLL.Interfaces;
using BookStore.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BookStore.WEB.Controllers
{
    [Authorize (Roles = "Admin")]
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IBookManager _manager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IBookManager manager, ILogger<AdminController> logger)
        {
            _logger = logger;
            this._manager = manager;
        }

        [HttpPost]
        public IActionResult AddBook(Book book)
        {
            try
            {
                this._manager.AddBook(book);
                return Ok(book);
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

        [HttpPut]
        public IActionResult UpdateBook(Book book)
        {
            try
            {
                this._manager.UpdateBook(book);
                return Ok(book);
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

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                this._manager.DeleteBook(id);
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

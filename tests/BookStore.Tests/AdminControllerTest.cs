using BookStore.BLL.Interfaces;
using BookStore.DAL.Models;
using BookStore.WEB.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace BookStore.Tests
{
    public class AdminControllerTest
    {
        [Fact]
        public void AddBook()
        {
            // Arrange
            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(x => x.AddBook(It.IsAny<Book>()))
                .Callback<Book>((book) => AddTestBook(book));

            var logger = new Mock<ILogger<AdminController>>();

            AdminController controller = new AdminController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, "testAdmin"), new Claim(ClaimTypes.Role, "Admin") },
                        "authTypeName")
                    )
                }
            };

            var book = new Book { Name = "D book", Text = "D text", Price = 1 };


            // Act
            var result = controller.AddBook(book);

            // Assert
            Assert.NotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
        }

        [Fact]
        public void UpdateBook()
        {
            // Arrange
            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(x => x.UpdateBook(It.IsAny<Book>()))
                .Callback<Book>((book) => UpdateTestBook(book));

            var logger = new Mock<ILogger<AdminController>>();

            AdminController controller = new AdminController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, "testAdmin"), new Claim(ClaimTypes.Role, "Admin") },
                        "authTypeName")
                    )
                }
            };

            var book = new Book {Id = 3, Name = "CC book", Text = "CC text", Price = 1 };


            // Act
            var result = controller.UpdateBook(book);

            // Assert
            Assert.NotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
        }

        [Fact]
        public void DeleteBook()
        {
            // Arrange
            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(x => x.DeleteBook(It.IsAny<int>()))
                .Callback<int>((bookId) => DeleteTestBook(bookId));

            var logger = new Mock<ILogger<AdminController>>();

            AdminController controller = new AdminController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, "testAdmin"), new Claim(ClaimTypes.Role, "Admin") },
                        "authTypeName")
                    )
                }
            };

            int bookId = 1;

            // Act
            var result = controller.DeleteBook(bookId);

            // Assert
            Assert.NotNull(result);

            var okObjectResult = result as OkResult;
            Assert.NotNull(okObjectResult);
        }

        private IEnumerable<Book> GetTestBooks()
        {
            var books = new List<Book>
            {
                new Book { Id=1, Name="Book A", Text="Book A text", Price=5, IsPurchased=false},
                new Book { Id=2, Name="Book B", Text="Book B text", Price=7, IsPurchased=false},
                new Book { Id=3, Name="Book C", Text="Book C text", Price=6, IsPurchased=false}
            };
            return books;
        }

        private void AddTestBook(Book book)
        {
            if(book.Id != 0)
            {
                throw new Exception();
            }
        }

        private void UpdateTestBook(Book book)
        {
            if (GetTestBooks().FirstOrDefault(b => book.Id == b.Id) == null)
            {
                throw new Exception();
            }
        }

        private void DeleteTestBook(int bookId)
        {
            if (GetTestBooks().FirstOrDefault(b => bookId == b.Id) == null)
            {
                throw new Exception();
            }
        }
    }
}

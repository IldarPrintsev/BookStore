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
    public class BookControllerTest
    {
        [Fact]
        public void GetBooks()
        {
            // Arrange
            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(manager => manager.GetBooks()).Returns(GetTestBooks());

            var logger = new Mock<ILogger<BookController>>();

            BookController controller = new BookController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, "testUser") },
                        "authTypeName")
                    )
                }
            };

            // Act
            var result = controller.GetBooks();

            // Assert
            Assert.NotNull(result);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var books = okObjectResult.Value as IEnumerable<Book>;
            Assert.Equal(GetTestBooks().ToList().Count, books.ToList().Count);
        }

        [Fact]
        public void GetBooksForAuthorizedUser()
        {
            // Arrange
            var userName = "testUser";

            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(manager => manager.GetBooks()).Returns(GetTestBooks());
            managerMock.Setup(x => x.MarkPurchasedBooks(It.IsAny<string>(), It.IsAny<IEnumerable<Book>>()))
                .Callback<string, IEnumerable<Book>>((email, books) => MarkPurchasedBooks(email, books));
            managerMock.Object.MarkPurchasedBooks(userName, GetTestBooks());

            var logger = new Mock<ILogger<BookController>>();

            BookController controller = new BookController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, userName) },
                        "authTypeName")
                    )
                }
            };

            // Act
            var result = controller.GetBooks();

            // Assert
            Assert.NotNull(result);

            Assert.Equal(controller.User.Identity.Name, userName);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var books = okObjectResult.Value as IEnumerable<Book>;
            Assert.True(books.ToList()[1].IsPurchased);
        }

        [Fact]
        public void UpdateSubscriptionToSubscribe()
        {
            // Arrange
            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(x => x.Subscribe(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((email, bookId) => Subscribe(email, bookId));

            var logger = new Mock<ILogger<BookController>>();

            BookController controller = new BookController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, "testUser") },
                        "authTypeName")
                    )
                }
            };

            var book = new Book { Id = 4, Name = "Book D", Text = "Book D text", Price = 6, IsPurchased = true };

            // Act
            var result = controller.UpdateSubscription(book);

            // Assert
            Assert.NotNull(result);

            var okResult = result as OkResult;
            Assert.NotNull(okResult);
        }

        [Fact]
        public void UpdateSubscriptionToUnsubscribe()
        {
            // Arrange
            var managerMock = new Mock<IBookManager>();
            managerMock.Setup(x => x.Subscribe(It.IsAny<string>(), It.IsAny<int>()))
                .Callback<string, int>((email, bookId) => Unsubscribe(email, bookId));

            var logger = new Mock<ILogger<BookController>>();

            BookController controller = new BookController(managerMock.Object, logger.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, "testUser") },
                        "authTypeName")
                    )
                }
            };

            var book = new Book { Id = 4, Name = "Book A", Text = "Book A text", Price = 5, IsPurchased = true };

            // Act
            var result = controller.UpdateSubscription(book);

            // Assert
            Assert.NotNull(result);

            var okResult = result as OkResult;
            Assert.NotNull(okResult);
        }

        private IEnumerable<Book> GetTestBooks()
        {
            var books = new List<Book>
            {
                new Book { Id=1, Name="Book A", Text="Book A text", Price=5, IsPurchased=false},
                new Book { Id=2, Name="Book B", Text="Book B text", Price=7, IsPurchased=false},
                new Book { Id=3, Name="Book C", Text="Book C text", Price=6, IsPurchased=false},
                new Book { Id=4, Name="Book D", Text="Book D text", Price=6, IsPurchased=true}
            };
            return books;
        }

        private void MarkPurchasedBooks(string email, IEnumerable<Book> books)
        {
            var userBooks = books.ToList()[1].IsPurchased = true;
        }

        private void Subscribe(string email, int bookId)
        {
            var books = GetTestBooks();
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book.IsPurchased)
            {
                throw new InvalidOperationException();
            }
        }

        private void Unsubscribe(string email, int bookId)
        {
            var books = GetTestBooks();
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (!book.IsPurchased)
            {
                throw new InvalidOperationException();
            }
        }

    }
}

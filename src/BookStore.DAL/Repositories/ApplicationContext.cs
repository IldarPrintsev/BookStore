using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<UserBook> UsersBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserBook>()
            .HasKey(t => new { t.UserId, t.BookId });

            modelBuilder.Entity<UserBook>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.Books)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserBook>()
                .HasOne(sc => sc.Book)
                .WithMany(c => c.Users)
                .HasForeignKey(sc => sc.BookId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin",
                    Password = "123",
                    Role = "Admin"
                });

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Name = "The catcher in the rye",
                    Text = "The Catcher in the Rye is a novel by J. D. Salinger, partially published in serial form in 1945–1946 and as a novel in 1951.",
                    Price = 5
                },
                new Book
                {
                    Id = 2,
                    Name = "Wuthering heights",
                    Text = "Wuthering Heights is a novel by Emily Brontë published in 1847 under her pseudonym Ellis Bell. Brontë's only finished novel, it was written between October 1845 and June 1846.",
                    Price = 6
                },
                new Book
                {
                    Id = 3,
                    Name = "Crime and punishment",
                    Text = "Crime and Punishment is a novel by the Russian author Fyodor Dostoevsky. It was first published in the literary journal The Russian Messenger in twelve monthly installments during 1866.",
                    Price = 7
                });
        }
    }
}

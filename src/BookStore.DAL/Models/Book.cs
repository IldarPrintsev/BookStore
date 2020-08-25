using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.DAL.Models
{
    public class Book
    {
        public Book()
        {
            Users = new List<UserBook>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public double Price { get; set; }

        public List<UserBook> Users { get; set; }


        [NotMapped]
        public bool IsPurchased { get; set; }
    }
}

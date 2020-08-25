using System.Collections.Generic;

namespace BookStore.DAL.Models
{
    public class User
    {
        public User()
        {
            Books = new List<UserBook>();
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public List<UserBook> Books { get; set; }
    }
}

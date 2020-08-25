using BookStore.DAL.Models;

namespace BookStore.BLL.Interfaces
{
    public interface ITokenManager
    {
        string GenerateAccessToken(User user);
    }
}

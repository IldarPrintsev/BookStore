using BookStore.DAL.Models;

namespace BookStore.BLL.Interfaces
{
    public interface IAuthManager
    {
        SignInResult SignIn(User user);

        bool SignUp(User user);
    }
}

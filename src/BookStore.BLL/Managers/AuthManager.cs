using BookStore.BLL.Interfaces;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Models;
using System;

namespace BookStore.BLL.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserRepository _repository;
        private readonly ITokenManager _tokenManager;

        public AuthManager(IUserRepository repository, ITokenManager tokenManager)
        {
            this._repository = repository;
            this._tokenManager = tokenManager;
        }

        public SignInResult SignIn(User user)
        {
            var result = new SignInResult();
            try
            {
                var dbUser = this._repository.Get(user.Email, user.Password);

                if (dbUser == null)
                {
                    result.Succeeded = false;

                    return result;
                }

                var token = this._tokenManager.GenerateAccessToken(dbUser);

                result.Succeeded = true;
                result.Token = token;

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurs during sign in: " + ex.Message, ex);
            }
        }

        public bool SignUp(User user)
        {
            try
            {
                if(user.Role == null)
                {
                    user.Role = "User";
                }

                var result = this._repository.Add(user);

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurs during sign up: " + ex.Message, ex);
            }
        }
    }
}

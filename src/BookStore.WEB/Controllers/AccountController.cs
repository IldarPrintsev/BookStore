using BookStore.BLL.Interfaces;
using BookStore.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace BookStore.WEB.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly IAuthManager _authManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
        {
            this._authManager = authManager;
            this._logger = logger;
        }

        [HttpPost, Route("signin")]
        public IActionResult SignIn([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            try
            {
                var signInResult = this._authManager.SignIn(user);

                if (!signInResult.Succeeded)
                {
                    return Unauthorized();
                }

                return Ok(new { token = signInResult.Token });
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

        [HttpPost, Route("signup")]
        public IActionResult SignUp([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid sign up request");
            }

            try
            {
                bool result = this._authManager.SignUp(user);

                if (!result)
                {
                    return BadRequest("This email address is already in use");
                }

                var signInResult = this._authManager.SignIn(user);

                if (!signInResult.Succeeded)
                {
                    return Unauthorized();
                }

                return Ok(new { token = signInResult.Token });
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

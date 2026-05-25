using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using DTOs.Request;

namespace AngularCWeb.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Service instance used for user-related business operations.
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the UserController class with the specified user service.
        /// </summary>
        /// <param name="userService">The service used for user-related operations.</param>
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        /// <summary>
        /// Authenticates a user using the provided login credentials.
        /// </summary>
        /// <param name="request">The login request containing email and password.</param>
        /// <returns>
        /// Returns a success response if authentication is successful; otherwise, returns an unauthorized response.
        /// </returns>
   

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginRequest))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = userService.Login(request.Email, request.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            return Ok(new LoginRequest
            {
                Password = "Login successful",
                Email = user.Email
            });
        }
    }
}

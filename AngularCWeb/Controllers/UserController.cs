using DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Implementations;
using Service.Services.Interfaces;

namespace AngularCWeb.Controllers
{
#nullable disable
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
        /// Logger instance used for application logging.
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// Initializes a new instance of the UserController class with the specified user service.
        /// </summary>
        /// <param name="userService">The service used for user-related operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record controller activities, errors, and diagnostic information.
        /// </param>
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            this.logger.LogInformation("Started {Controller} - {Method}",nameof(UserController),nameof(Login));
            var user = userService.Login(request.Email, request.Password);

            if (user == null)
            {
                this.logger.LogWarning("Invalid Login Attempt {Controller} - {Method}",nameof(UserController),nameof(Login));
                return BadRequest("Invalid Credentials");
            }

            this.logger.LogInformation("Ended {Controller} - {Method}",nameof(UserController),nameof(Login));
            return Ok(new
            {
                message = "Login Success",
                //Token = token,
                Role = user.Role,
                FullName = user.FullName
            });
        }
    }
}

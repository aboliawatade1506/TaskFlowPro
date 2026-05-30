using Data.Model;
using Data.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Service.Services.Interfaces;

namespace Service.Services.Implementations
{
#nullable disable

    /// <summary>
    /// Service implementation for user-related business operations.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Repository instance used to perform user-related data access operations.
        /// </summary>
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Logger instance used to record information, warnings, and errors related to user service operations.
        /// </summary>
        private readonly ILogger<UserService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class
        /// with the specified user repository and logger.
        /// </summary>
        /// <param name="userRepository">
        /// Repository used to perform user-related data access operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record application events, diagnostic information,warnings, and exceptions for user service operations.
        /// </param>
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Authenticates a user by validating the provided email and password through the user repository.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// Returns the authenticated user details if the credentials are valid; otherwise, returns null.
        /// </returns>
        public User Login(string email, string password)
        {
            this.logger.LogInformation("Started {Service} - {Method} Email: {Email}",nameof(UserService),nameof(Login),email);

            var user =userRepository.Login(email, password);

            this.logger.LogInformation("Ended {Service} - {Method} Email: {Email} Response: {@Response}",nameof(UserService),nameof(Login),email,user);

            return user;
        }
    }
}

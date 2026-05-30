using Data.Model;
using Data.Repository.Interfaces;
using DataAccess.Context;
using Microsoft.Extensions.Logging;

namespace Data.Repository.Implementations
{
#nullable disable

    /// <summary>
    /// Repository implementation for user-related data access operations.
    /// </summary>
    public class UserRepository :IUserRepository
    {
        /// <summary>
        /// Database context used to access application data.
        /// </summary>
        private readonly TaskdbContext _context;

        /// <summary>
        ///  Logger used to record user repository operations, errors, and diagnostic information.
        /// </summary>
        private readonly ILogger<UserRepository> logger;

        /// <summary>
        /// Initializes a new instance of the UserRepository class with the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used for data access operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record repository activities, errors, warnings, and debugging information.
        /// </param>
        public UserRepository(TaskdbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Authenticates a user by validating the provided email and password credentials.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// Returns the authenticated user details if the credentials are valid; otherwise, returns null.
        /// </returns>
        public User Login(string email, string password)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Email: {Email}",nameof(UserRepository),nameof(Login),email);

            var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            this.logger.LogInformation("Ended {Repository} - {Method} Email: {Email} Response: {@Response}",nameof(UserRepository),nameof(Login),email,user);
           
            return user;
        }
    }
}

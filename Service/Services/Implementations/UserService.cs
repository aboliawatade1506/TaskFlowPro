using Data.Model;
using Data.Repository.Interfaces;
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
        /// Initializes a new instance of the UserService class with the specified user repository.
        /// </summary>
        /// <param name="userRepository">The repository used for user-related data access operations.</param>
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
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
            return userRepository.Login(email, password);
        }
    }
}

using Data.Model;
using DataAccess.Context;
using Data.Repository.Interfaces;

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
        /// Initializes a new instance of the UserRepository class with the specified database context.
        /// </summary>
        /// <param name="context">The database context used for data access operations.</param>
        public UserRepository(TaskdbContext context)
        {
            _context = context;
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
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}

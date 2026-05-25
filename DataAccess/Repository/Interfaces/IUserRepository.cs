using Data.Model;

namespace Data.Repository.Interfaces
{
    /// <summary>
    /// Defines the contract for user-related data access operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Authenticates a user by validating the provided email and password credentials.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// Returns the authenticated user details if the credentials are valid; otherwise, returns null.
        /// </returns>
        public User Login(string email, string password);
    }
}

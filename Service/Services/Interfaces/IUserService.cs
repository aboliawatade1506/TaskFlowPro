using Data.Model;

namespace Service.Services.Interfaces
{
#nullable disable

    /// <summary>
    /// Defines the contract for user-related business operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user using the provided email and password credentials.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        /// Returns the authenticated user details if the credentials are valid; otherwise, returns null.
        /// </returns>
        public User Login(string email, string password);

    }
}

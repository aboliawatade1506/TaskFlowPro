namespace DTOs.Request
{
#nullable disable

    /// <summary>
    /// Represents the request model used for user login authentication.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }
    }
}

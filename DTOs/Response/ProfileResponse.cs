namespace DTOs.Response
{
#nullable disable
    /// <summary>
    /// Response DTO used to update user profile details.
    /// </summary>
    public class ProfileResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the full name of the user.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the role assigned to the user.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the profile image path or URL of the user.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the user's password.
        /// Used for password validation during profile update.
        /// </summary>
        public string Password {  get; set; }
    }
}

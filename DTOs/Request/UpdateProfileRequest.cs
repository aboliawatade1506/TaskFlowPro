namespace DTOs.Request
{
#nullable disable
    /// <summary>
    /// Request DTO used to update user profile details.
    /// </summary>
    public class UpdateProfileRequest
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
        ///  Gets or sets the profile image path or URL of the user.
        /// </summary>
        public string ProfileImage { get; set; }

        /// <summary>
        /// Gets or sets the current password of the user for verification before updating the profile.
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password that the user wants to update.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password to validate the new password entered by the user.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}

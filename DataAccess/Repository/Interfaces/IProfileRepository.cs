using DTOs.Request;
using DTOs.Response;

namespace AngularCWeb.DataAccess.Repository.Interfaces
{
    /// <summary>
    /// Defines methods for profile related data operations.
    /// </summary>
    public interface IProfileRepository
    {

        /// <summary>
        /// Retrieves profile details using user identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the user.
        /// </param>
        /// <returns>
        /// Returns profile information for the specified user.
        /// </returns>
        ProfileResponse GetProfile(Guid id);

        /// <summary>
        /// Retrieves profile details using email address.
        /// </summary>
        /// <param name="email">
        /// Email address of the user.
        /// </param>
        /// <returns>
        /// Returns profile information for the specified email.
        /// </returns>
        ProfileResponse GetProfile(string email);

        /// <summary>
        /// Updates user profile information.
        /// </summary>
        /// <param name="request">
        /// Contains updated profile details.
        /// </param>
        /// <returns>
        /// Returns true if the profile is updated successfully;
        /// otherwise returns false.
        /// </returns>
        bool UpdateProfile(UpdateProfileRequest request);
    }
}

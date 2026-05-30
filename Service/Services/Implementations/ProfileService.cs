using AngularCWeb.DataAccess.Repository.Interfaces;
using DTOs.Request;
using DTOs.Response;
using Microsoft.Extensions.Logging;
using Service.Services.Interfaces;

namespace Service.Services.Implementations
{
    /// <summary>
    /// Provides business logic for profile related operations.
    /// </summary>
    public class ProfileService :IProfileService
    {
        /// <summary>
        /// Repository used to perform profile related data operations.
        /// </summary>
        private readonly IProfileRepository profileRepository;

        /// <summary>
        /// Logger used to record profile service operations, errors, and diagnostic information.
        /// </summary>
        private readonly ILogger<ProfileService> logger;

        /// <summary>
        /// Initializes a new instance of the ProfileService class.
        /// </summary>
        /// <param name="_profileRepository">
        /// Repository used for profile-related operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record service activities, errors, warnings, and debugging information.
        /// </param>
        public ProfileService(IProfileRepository _profileRepository, ILogger<ProfileService> logger)
        {
            profileRepository = _profileRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves profile details using user identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the user.
        /// </param>
        /// <returns>
        /// Returns profile information for the specified user.
        /// </returns>

        public ProfileResponse GetProfile(Guid id)
        {
            this.logger.LogInformation("Started {Service} - {Method} UserId: {UserId}",nameof(ProfileService),nameof(GetProfile),id);

            var response = profileRepository.GetProfile(id);

            this.logger.LogInformation("Ended {Service} - {Method} UserId: {UserId} Response: {@Response}",nameof(ProfileService),nameof(GetProfile),id,response);

            return response;
        }

        /// <summary>
        /// Retrieves profile details using email address.
        /// </summary>
        /// <param name="email">
        /// Email address of the user.
        /// </param>
        /// <returns>
        /// Returns profile information for the specified email.
        /// </returns>
        public ProfileResponse GetProfile(string email)
        {
            this.logger.LogInformation("Started {Service} - {Method} Email: {Email}",nameof(ProfileService),nameof(GetProfile),email);

            var response = profileRepository.GetProfile(email);

            this.logger.LogInformation("Ended {Service} - {Method} Email: {Email} Response: {@Response}",nameof(ProfileService),nameof(GetProfile),email,response);

            return response;
        }

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
        public bool UpdateProfile(UpdateProfileRequest request)
        {
            this.logger.LogInformation("Started {Service} - {Method} Request: {@Request}",nameof(ProfileService),nameof(UpdateProfile),request);

            var result = profileRepository.UpdateProfile(request);

            this.logger.LogInformation("Ended {Service} - {Method} UserId: {UserId} Result: {Result}",nameof(ProfileService),nameof(UpdateProfile),request.Id,result);

            return result;
        }
    }
}

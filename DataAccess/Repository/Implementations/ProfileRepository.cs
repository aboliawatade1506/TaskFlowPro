using AngularCWeb.DataAccess.Repository.Interfaces;
using DataAccess.Context;
using DTOs.Request;
using DTOs.Response;
using Microsoft.Extensions.Logging;

namespace AngularCWeb.DataAccess.Repository.Implementations
{
#nullable disable
    /// <summary>
    /// Repository responsible for managing profile related data operations.
    /// </summary>
    public class ProfileRepository :IProfileRepository
    {
        /// <summary>
        /// Database context used to access user profile data.
        /// </summary>
        private readonly TaskdbContext context;

        /// <summary>
        /// Logger used to record profile repository operations, errors, and diagnostic information.
        /// </summary>
        private readonly ILogger<ProfileRepository> logger;

        /// <summary>
        /// Initializes a new instance of the ProfileRepository class.
        /// </summary>
        /// <param name="context">
        /// Database context used for profile operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record repository activities, errors, and debugging information.
        /// </param>
        public ProfileRepository(TaskdbContext context, ILogger<ProfileRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves profile details using user identifier.
        /// </summary>
        /// <param name="id">
        /// Unique identifier of the user.
        /// </param>
        /// <returns>
        /// Returns profile information if found; otherwise null.
        /// </returns>
        public ProfileResponse GetProfile(Guid id)
        {
            this.logger.LogInformation("Started {Repository} - {Method} UserId: {UserId}",nameof(ProfileRepository),nameof(GetProfile),id);
            var profile = context.Users.Where(x => x.Id == id).Select(x => new ProfileResponse
            {
              Id = x.Id,
              FullName = x.FullName,
              Email = x.Email,
              Role = x.Role,
              Password = x.Password,
              ProfileImage = x.ProfileImage
            }).FirstOrDefault();

            this.logger.LogInformation("Ended {Repository} - {Method} UserId: {UserId}",nameof(ProfileRepository),nameof(GetProfile),id);
            return profile;
        }

        /// <summary>
        /// Retrieves profile details using email address.
        /// </summary>
        /// <param name="email">
        /// Email address of the user.
        /// </param>
        /// <returns>
        /// Returns profile information if found; otherwise null.
        /// </returns>
        public ProfileResponse GetProfile(string email)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Email: {Email}",nameof(ProfileRepository),nameof(GetProfile),email);

            var profile = context.Users
               .Where(x => x.Email == email)
                .Select(x => new ProfileResponse
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    Role = x.Role,
                    Password = x.Password,
                    ProfileImage = x.ProfileImage
                })
                .FirstOrDefault();

            this.logger.LogInformation("Ended {Repository} - {Method} Email: {Email}",nameof(ProfileRepository),nameof(GetProfile),email);

            return profile;
        }

        /// <summary>
        /// Updates user profile information including
        /// name, email, role, password and profile image.
        /// </summary>
        /// <param name="request">
        /// Contains updated profile details.
        /// </param>
        /// <returns>
        /// Returns true if profile is updated successfully;
        /// otherwise returns false.
        /// </returns>
        public bool UpdateProfile(UpdateProfileRequest request)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Request: {@Request}",nameof(ProfileRepository),nameof(UpdateProfile),request);
           
            var user = context.Users.FirstOrDefault(x => x.Id == request.Id);

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.Role = request.Role;
            user.Password = request.NewPassword;
            user.ProfileImage = request.ProfileImage;

            context.SaveChanges();

            this.logger.LogInformation("Ended {Repository} - {Method} UserId: {UserId}",nameof(ProfileRepository),nameof(UpdateProfile),request.Id);
           
            return true;
        }
    }
}

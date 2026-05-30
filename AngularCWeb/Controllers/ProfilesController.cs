using DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace AngularCWeb.Controllers
{
#nullable disable

    /// <summary>
    /// Handles profile related operations such as
    /// retrieving and updating user profile details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        ///  Provides profile related business operations.
        /// </summary>
        private readonly IProfileService profileService;

        /// <summary>
        /// Logger instance used for application logging.
        /// </summary>
        private readonly ILogger<ProfileController> logger;

        /// <summary>
        /// Initializes a new instance of the ProfileController class.
        /// </summary>
        /// <param name="_profileService">
        /// Service used to perform profile related operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record controller activities, errors, and diagnostic information.
        /// </param>
        public ProfileController(IProfileService _profileService, ILogger<ProfileController> logger)
        {
            profileService = _profileService;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves user profile details using the specified email address.
        /// </summary>
        /// <param name="email">
        /// Email address of the user whose profile information is to be retrieved.
        /// </param>
        /// <returns>
        /// Returns the user profile details if the user exists; otherwise returns NotFound.
        /// </returns>
        [HttpGet("GetProfile/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetProfile(string email)
        {
            this.logger.LogInformation("Started {Controller} - {Method} Email: {Email}", nameof(ProfileController),nameof(GetProfile),email);
            var user = profileService.GetProfile(email);

            if (user == null)
            {
                this.logger.LogWarning("User Not Found {Controller} - {Method} Email: {Email}", nameof(ProfileController),nameof(GetProfile),email);
                return NotFound();
            }
            this.logger.LogInformation("Ended {Controller} - {Method} Email: {Email}", nameof(ProfileController),nameof(GetProfile),email);
            return Ok(user);
        }

        /// <summary>
        /// Updates user profile details and validates password information.
        /// </summary>
        /// <param name="request">
        /// Contains profile information and password details.
        /// </param>
        /// <returns>
        /// Returns success message if profile is updated successfully;
        /// otherwise returns an appropriate error response.
        /// </returns>
        [HttpPut("UpdateProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProfile(UpdateProfileRequest request)
        {
            this.logger.LogInformation("Started {Controller} - {Method} Request: {@Request}", nameof(ProfileController),nameof(UpdateProfile),request);
            var profile = profileService.GetProfile(request.Id);

            if (profile == null)
            {
                this.logger.LogWarning("User Not Found {Controller} - {Method} Request: {@Request}", nameof(ProfileController),nameof(UpdateProfile),request);
                return NotFound("User Not Found");
            }

            // Current Password Check
            if (profile.Password != request.CurrentPassword)
            {
                this.logger.LogWarning("Current Password Incorrect {Controller} - {Method} UserId: {UserId}", nameof(ProfileController),nameof(UpdateProfile), request.Id);
                return BadRequest("Current Password Incorrect");
            }

            // New Password & Confirm Password Check
            if (request.NewPassword != request.ConfirmPassword)
            {
                this.logger.LogWarning("Password Mismatch {Controller} - {Method} UserId: {UserId}", nameof(ProfileController),nameof(UpdateProfile),request.Id);
                return BadRequest("New Password and Confirm Password not match");
            }

            var result = profileService.UpdateProfile(request);

            if (result)
            {
                this.logger.LogInformation("Ended {Controller} - {Method} UserId: {UserId}", nameof(ProfileController),nameof(UpdateProfile),request.Id);
                return Ok(new
                {
                    Message = "Profile Updated Successfully"
                });
            }
            this.logger.LogWarning("Profile Update Failed {Controller} -{Method} UserId: {UserId}", nameof(ProfileController),nameof(UpdateProfile), request.Id);
            return BadRequest("Profile Update Failed");
        }
    }
}

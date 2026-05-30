using AngularCWeb.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AngularCWeb.Controllers
{
    /// <summary>
    /// API controller for handling dashboard related requests.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        /// <summary>
        /// Service used for dashboard related operations.
        /// </summary>
        private readonly IDashboardService dashboardService;

        /// <summary>
        /// Logger instance used for application logging.
        /// </summary>
        private readonly ILogger<DashboardController> logger;

        /// <summary>
        /// Initializes a new instance of DashboardController class.
        /// </summary>
        /// <param name="dashboardService">
        /// Represents the dashboard service instance.
        /// </param>
        /// <param name="logger">
        /// Represents the logger used to record controller activities, errors, and diagnostic information.
        /// </param>
        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger)
        {
            this.dashboardService = dashboardService;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves dashboard data based on the specified user role.
        /// </summary>
        /// <param name="role">
        /// Role of the logged-in user.
        /// </param>
        /// <returns>
        /// Returns dashboard statistics and summary information.
        /// </returns>
        [HttpGet("{role}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetDashboardData(string role)
        {
            this.logger.LogInformation("Started {Controller} - {Method} Role: {Role}", nameof(DashboardController),nameof(GetDashboardData),role);
            var data = dashboardService.GetDashBoardData(role);
            this.logger.LogInformation("Ended {Controller} - {Method} Role: {Role}",nameof(DashboardController),nameof(GetDashboardData),role);
            return Ok(data);
        }
    }
}

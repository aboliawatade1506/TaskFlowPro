using AngularCWeb.DataAccess.Repository.Interfaces;
using AngularCWeb.Service.Services.Interfaces;
using DTOs.Response;
using Microsoft.Extensions.Logging;

namespace AngularCWeb.Service.Services.Implementations
{
    /// <summary>
    /// Service class for handling dashboard related business logic.
    /// </summary>
    public class DashboardService:IDashboardService
    {
        /// <summary>
        /// Repository used for accessing dashboard data.
        /// </summary>
        private readonly IDashboardRepository dashboardrepository;

        /// <summary>
        /// Logger used to record dashboard service operations, errors, and diagnostic information.
        /// </summary>
        private readonly ILogger<DashboardService> logger;

        /// <summary>
        /// Initializes a new instance of the DashboardService class.
        /// </summary>
        /// <param name="dashboardRepository">
        /// Represents the dashboard repository used for dashboard data operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record service activities, errors, warnings, and debugging information.
        /// </param>
        public DashboardService(IDashboardRepository dashboardRepository, ILogger<DashboardService> logger)
        {
            this.dashboardrepository = dashboardRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves dashboard task summary details.
        /// </summary>
        /// <returns>
        /// Returns total, completed, pending and in progress task counts.
        /// </returns>
        public DashBoardResponse GetDashBoardData(string role)
        {
            this.logger.LogInformation("Started {Service} - {Method} Role: {Role}",nameof(DashboardService),nameof(GetDashBoardData),role);

            var response =dashboardrepository.GetDashboardData(role);

            this.logger.LogInformation("Ended {Service} - {Method} Role: {Role} Response: {@Response}",nameof(DashboardService),nameof(GetDashBoardData),role,response);

            return response;
        }
    }
}

using AngularCWeb.DataAccess.Repository.Interfaces;
using DataAccess.Context;
using DTOs.Response;
using Microsoft.Extensions.Logging;

namespace AngularWeb.DataAccess.Repository.Implementations
{
    /// <summary>
    /// Repository class for managing dashboard related operations.
    /// </summary>
    public class DashboardRepository: IDashboardRepository
    {
        /// <summary>
        /// Database context used for accessing task data.
        /// </summary>
        private readonly TaskdbContext context;

        /// <summary>
        /// Logger for dashboard repository activities.
        /// </summary>
        private readonly ILogger<DashboardRepository> logger;

        /// <summary>
        /// Initializes a new instance of DashboardRepository class.
        /// </summary>
        /// <param name="context">
        /// Represents the database context instance.
        /// </param>
        /// <param name="logger">
        /// Represents the logger used to record repository operations, errors, and diagnostic information.
        /// </param>
        public DashboardRepository(TaskdbContext context, ILogger<DashboardRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves dashboard task summary details.
        /// </summary>
        /// <returns>
        /// Returns total, completed, pending and in progress task counts.
        /// </returns>
        public DashBoardResponse GetDashboardData(string role)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Role: {Role}", nameof(DashboardRepository),nameof(GetDashboardData),role);
            DashBoardResponse dash = new DashBoardResponse();

            if (role == "Admin")
            {
                dash.TotalTasks = context.EmpTask.Count();

                dash.CompletedTasks = context.EmpTask.Count(x => x.TaskStatus == "Completed");

                dash.PendingTasks = context.EmpTask.Count(x => x.TaskStatus == "Pending");

                dash.InProgressTasks = context.EmpTask.Count(x => x.TaskStatus == "In Progress");
            }
            else
            {
                dash.TotalTasks = context.EmpTask.Count(x => x.AssignBy == role);

                dash.CompletedTasks = context.EmpTask.Count(x => x.AssignBy == role &&x.TaskStatus == "Completed");

                dash.PendingTasks = context.EmpTask.Count(x => x.AssignBy == role &&x.TaskStatus == "Pending");

                dash.InProgressTasks = context.EmpTask.Count(x => x.AssignBy == role &&x.TaskStatus == "In Progress");
            }
            this.logger.LogInformation("Ended {Repository} - {Method} Role: {Role}", nameof(DashboardRepository),nameof(GetDashboardData),role);

            return dash;
        }

    }
}

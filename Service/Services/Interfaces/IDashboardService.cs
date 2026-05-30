using DTOs.Response;

namespace AngularCWeb.Service.Services.Interfaces
{
    /// <summary>
    /// Interface for dashboard related service operations.
    /// </summary>
    public interface IDashboardService
    {
        /// <summary>
        /// Retrieves dashboard task summary details.
        /// </summary>
        /// <returns>
        /// Returns total, completed, pending and in progress task counts.
        /// </returns>
        DashBoardResponse GetDashBoardData(string role);
    }
}

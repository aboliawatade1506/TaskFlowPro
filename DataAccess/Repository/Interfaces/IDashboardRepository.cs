using DTOs.Response;

namespace AngularCWeb.DataAccess.Repository.Interfaces
{
    /// <summary>
    /// Defines dashboard related repository methods.
    /// </summary>
    public interface IDashboardRepository
    {
        /// <summary>
        /// Retrieves dashboard task summary details.
        /// </summary>
        /// <returns>
        /// Returns total, completed, pending and in progress task counts.
        /// </returns>
        DashBoardResponse GetDashboardData(string role); 
    }
}

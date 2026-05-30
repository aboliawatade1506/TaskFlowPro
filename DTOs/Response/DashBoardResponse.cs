namespace DTOs.Response
{
    /// <summary>
    /// Dashboard response DTO used to return task summary details.
    /// </summary>
    public class DashBoardResponse
    {
        /// <summary>
        ///  Gets or sets the total number of tasks.
        /// </summary>
        public int TotalTasks {  get; set; }

        /// <summary>
        /// Gets or sets the total number of completed tasks.
        /// </summary>
        public int CompletedTasks { get; set; }

        /// <summary>
        /// Gets or sets the total number of pending tasks.
        /// </summary>
        public int PendingTasks { get; set; }

        /// <summary>
        /// Gets or sets the total number of tasks currently in progress.
        /// </summary>
        public int InProgressTasks { get; set; }

    }
}

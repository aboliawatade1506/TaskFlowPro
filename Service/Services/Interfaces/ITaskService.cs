using Data.Model;

namespace Service.Services.Interfaces
{
#nullable disable

    /// <summary>
    /// Defines the contract for task-related business operations.
    /// </summary>
    public interface ITaskService
    {
        /// <summary>
        /// Retrieves a task by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>
        /// Returns the task details if found; otherwise, returns null.
        /// </returns>
        Task<ModelTask> GetTaskByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        Task<IEnumerable<ModelTask>> GetAllTaskAsync();

        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        Task<IEnumerable<ModelTask>> GetCompletedTkAsync();

        /// <summary>
        /// Creates a new task asynchronously.
        /// </summary>
        /// <param name="entity">The task entity to be created.</param>
        /// <returns>
        /// Represents the asynchronous create operation.
        /// </returns>
        Task CreateTaskAsync(ModelTask entity);

        /// <summary>
        /// Updates an existing task asynchronously.
        /// </summary>
        /// <param name="entity">The task entity containing updated information.</param>
        /// <returns>
        /// Represents the asynchronous update operation.
        /// </returns>
        Task UpdateTaskAsync(ModelTask entity);

        /// <summary>
        /// Deletes a task asynchronously using its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to be deleted.</param>
        /// <returns>
        /// Represents the asynchronous delete operation.
        /// </returns>
        Task DeleteTaskAsync(Guid id);

    }
}

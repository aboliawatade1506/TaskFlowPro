using Data.Model;
using Data.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using Service.Services.Interfaces;

namespace Service.Services.Implementations
{
#nullable disable

    /// <summary>
    /// Service implementation for task-related business operations.
    /// </summary>
    public class TaskService : ITaskService
    {
        /// <summary>
        /// Repository instance used to perform task-related data access operations.
        /// </summary>
        private readonly ITaskRepository taskRepository;

        /// <summary>
        /// Logger instance used to record information, warnings, and errors
        /// </summary>
        private readonly ILogger<TaskService> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskService"/> class
        /// with the specified task repository and logger.
        /// </summary>
        /// <param name="taskRepository">
        /// Repository used to perform task-related data access operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record application events, diagnostic information,warnings, and exceptions for task service operations.
        /// </param>
        public TaskService (ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetAllTaskAsync(string role)
        {
            this.logger.LogInformation("Started {Service} - {Method} Role: {Role}",nameof(TaskService),nameof(GetAllTaskAsync),role);

            var tasks = await taskRepository.GetAllTask(role);

            this.logger.LogInformation("Ended {Service} - {Method} Role: {Role} Count: {Count}",nameof(TaskService),nameof(GetAllTaskAsync),role,tasks.Count());

            return tasks;
        }

        /// <summary>
        /// Retrieves a task by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>
        /// Returns the task details if found; otherwise, returns null.
        /// </returns>
        public async Task<ModelTask> GetTaskByIdAsync(Guid id)
        {
            this.logger.LogInformation("Started {Service} - {Method} TaskId: {TaskId}",nameof(TaskService),nameof(GetTaskByIdAsync),id);

            var task = await taskRepository.GetByIdAsync(id);

            this.logger.LogInformation("Ended {Service} - {Method} TaskId: {TaskId} Response: {@Response}",nameof(TaskService),nameof(GetTaskByIdAsync),id,task);

            return task;
        }

        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetCompletedTkAsync()
        {
            this.logger.LogInformation("Started {Service} - {Method}",nameof(TaskService),nameof(GetCompletedTkAsync));

            var tasks = await taskRepository.GetCompletedTasksAsync();

            this.logger.LogInformation("Ended {Service} - {Method} Count: {Count}",nameof(TaskService),nameof(GetCompletedTkAsync),tasks.Count());

            return tasks;
        }

        /// <summary>
        /// Creates a new task asynchronously.
        /// </summary>
        /// <param name="entity">The task entity to be created.</param>
        /// <returns>
        /// Represents the asynchronous create operation.
        /// </returns>
        public async Task CreateTaskAsync(ModelTask entity)
        {
            this.logger.LogInformation("Started {Service} - {Method} Request: {@Request}",nameof(TaskService),nameof(CreateTaskAsync),entity);

            await taskRepository.AddTaskAsync(entity);

            this.logger.LogInformation("Ended {Service} - {Method} TaskId: {TaskId}",nameof(TaskService),nameof(CreateTaskAsync),entity.TaskId);
        }

        /// <summary>
        /// Updates an existing task asynchronously.
        /// </summary>
        /// <param name="entity">The task entity containing updated information.</param>
        /// <returns>
        /// Represents the asynchronous update operation.
        /// </returns>
        public async Task UpdateTaskAsync(ModelTask entity)
        {
            this.logger.LogInformation("Started {Service} - {Method} Request: {@Request}",nameof(TaskService),nameof(UpdateTaskAsync),entity);

            await taskRepository.UpdateTaskAsync(entity);

            this.logger.LogInformation("Ended {Service} - {Method} TaskId: {TaskId}",nameof(TaskService),nameof(UpdateTaskAsync),entity.TaskId);
        }

        /// <summary>
        /// Deletes a task asynchronously using its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to be deleted.</param>
        /// <returns>
        /// Represents the asynchronous delete operation.
        /// </returns>
        public async Task DeleteTaskAsync(Guid id)
        {
            this.logger.LogInformation("Started {Service} - {Method} TaskId: {TaskId}",nameof(TaskService),nameof(DeleteTaskAsync),id);

            await taskRepository.DeleteTaskAsync(id);

            this.logger.LogInformation("Ended {Service} - {Method} TaskId: {TaskId}",nameof(TaskService),nameof(DeleteTaskAsync),id);
        }

        /// <summary>
        /// Retrieves tasks based on the specified user role.
        /// </summary>
        /// <param name="role">
        /// Role of the user.
        /// </param>
        /// <returns>
        /// Returns a collection of tasks associated with the specified role.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetTasksByRoleAsync(string role)
        {
            this.logger.LogInformation("Started {Service} - {Method} Role: {Role}",nameof(TaskService),nameof(GetTasksByRoleAsync),role);

            var tasks = await taskRepository.GetTasksByRoleAsync(role);

            this.logger.LogInformation("Ended {Service} - {Method} Role: {Role} Count: {Count}",nameof(TaskService),nameof(GetTasksByRoleAsync),role,tasks.Count());

            return tasks;
        }
    }
}

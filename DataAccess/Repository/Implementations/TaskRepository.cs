using Data.Model;
using Data.Repository.Interfaces;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Data.Repository.Implementations
{
#nullable disable

    /// <summary>
    /// Repository implementation for task-related data access operations.
    /// </summary>
    public class TaskRepository : ITaskRepository 
    {
        /// <summary>
        /// Database context used to access task-related data.
        /// </summary>
        private readonly TaskdbContext _Context;

        /// <summary>
        /// Logger used to record task repository operations, errors, and diagnostic information.
        /// </summary>
        private readonly ILogger<TaskRepository> logger;

        /// <summary>
        /// Initializes a new instance of the TaskRepository class with the specified database context.
        /// </summary>
        /// <param name="dbContext">The database context used for task data access operations.</param>
        /// <param name="logger">
        /// Logger used to record repository activities, errors, warnings, and debugging information.
        /// </param>
        public TaskRepository(TaskdbContext dbContext, ILogger<TaskRepository> logger)   
        {
            _Context = dbContext;
            this.logger = logger;
        }

        /// <summary>
        /// Retrieves a task by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>
        /// Returns the task details if found; otherwise, returns null.
        /// </returns>
        /// <returns></returns>
        public async Task<ModelTask> GetByIdAsync(Guid id)
        {
            this.logger.LogInformation("Started {Repository} - {Method} TaskId: {TaskId}",nameof(TaskRepository),nameof(GetByIdAsync),id);
           
            var task = await _Context.EmpTask.FindAsync(id);
           
            this.logger.LogInformation("Ended {Repository} - {Method} TaskId: {TaskId} Response: {@Response}",nameof(TaskRepository),nameof(GetByIdAsync),id,task);
           
            return task;
        }

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetAllTask(string role)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Role: {Role}",nameof(TaskRepository),nameof(GetAllTask),role);

            IEnumerable<ModelTask> tasks;

            if (role == "Admin")
            {
                tasks = await _Context.EmpTask.ToListAsync();
            }
            else
            {
                tasks = await _Context.EmpTask.Where(x => x.AssignBy == "Developer").ToListAsync();
            }

            this.logger.LogInformation("Ended {Repository} - {Method} Role: {Role} Count: {Count}",nameof(TaskRepository),nameof(GetAllTask),role,tasks.Count());

            return tasks;
        }

        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetCompletedTasksAsync()
        {
            this.logger.LogInformation("Started {Repository} - {Method}",nameof(TaskRepository),nameof(GetCompletedTasksAsync));

            var tasks = await _Context.EmpTask.ToListAsync();

            this.logger.LogInformation("Ended {Repository} - {Method} Count: {Count}",nameof(TaskRepository),nameof(GetCompletedTasksAsync),tasks.Count);

            return tasks;
        }

        /// <summary>
        /// Creates a new task asynchronously.
        /// </summary>
        /// <param name="entity">The task entity to be created.</param>
        /// <returns>
        /// Represents the asynchronous create operation.
        /// </returns>
        public async Task AddTaskAsync(ModelTask entity)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Request: {@Request}",nameof(TaskRepository),nameof(AddTaskAsync),entity);

            await _Context.AddAsync(entity);
            await _Context.SaveChangesAsync();

            this.logger.LogInformation("Ended {Repository} - {Method} TaskId: {TaskId}",nameof(TaskRepository),nameof(AddTaskAsync),entity.TaskId);
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
            this.logger.LogInformation("Started {Repository} - {Method} Request: {@Request}",nameof(TaskRepository),nameof(UpdateTaskAsync),entity);

            _Context.Update(entity);
            await _Context.SaveChangesAsync();

            this.logger.LogInformation("Ended {Repository} - {Method} TaskId: {TaskId}",nameof(TaskRepository),nameof(UpdateTaskAsync),entity.TaskId);
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
            this.logger.LogInformation("Started {Repository} - {Method} TaskId: {TaskId}",nameof(TaskRepository),nameof(DeleteTaskAsync), id);

            var entity = await GetByIdAsync(id);

            if (entity != null)
            {
                _Context.Remove(entity);
                await _Context.SaveChangesAsync();

                this.logger.LogInformation("Ended {Repository} - {Method} TaskId: {TaskId}",nameof(TaskRepository),nameof(DeleteTaskAsync),id);
            }
            else
            {
                this.logger.LogWarning(
                    "Task Not Found {Repository} - {Method} TaskId: {TaskId}",nameof(TaskRepository),nameof(DeleteTaskAsync),id);
            }
        }

        /// <summary>
        /// Retrieves tasks based on the specified user role.
        /// </summary>
        /// <param name="role">
        /// Role of the user.
        /// </param>
        /// <returns>
        /// Returns all tasks for Admin users;
        /// otherwise returns tasks assigned to the specified role.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetTasksByRoleAsync(string role)
        {
            this.logger.LogInformation("Started {Repository} - {Method} Role: {Role}",nameof(TaskRepository),nameof(GetTasksByRoleAsync),role);

            IEnumerable<ModelTask> tasks;

            if (role == "Admin")
            {
                tasks = await _Context.EmpTask.ToListAsync();
            }
            else
            {
                tasks = await _Context.EmpTask.Where(x => x.AssignBy == role).ToListAsync();
            }

            this.logger.LogInformation("Ended {Repository} - {Method} Role: {Role} Count: {Count}",nameof(TaskRepository),nameof(GetTasksByRoleAsync),role,tasks.Count());

            return tasks;
        }
    }
}

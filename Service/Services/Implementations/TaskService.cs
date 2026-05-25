using Data.Model;
using Service.Services.Interfaces;
using Data.Repository.Interfaces;

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
        /// Initializes a new instance of the TaskService class with the specified task repository.
        /// </summary>
        /// <param name="taskRepository">The repository used for task-related data access operations.</param>
        public TaskService (ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetAllTaskAsync()
        {
            return await taskRepository.GetAllTask();
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
            return await taskRepository.GetByIdAsync(id);
        }


        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetCompletedTkAsync()
        {
            return await taskRepository.GetCompletedTasksAsync();
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
            //if (string.IsNullOrWhiteSpace(entity.TaskName))
            //{
            //    throw new ArgumentException("Task Name is required.");
            //}
            await taskRepository.AddTaskAsync(entity);
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
            //if (string.IsNullOrWhiteSpace(entity.TaskName))
            //{
            //    throw new ArgumentException("Task Name is required.");
            //}
            await taskRepository.UpdateTaskAsync(entity);
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
            await taskRepository.DeleteTaskAsync(id);
        }
    }
}

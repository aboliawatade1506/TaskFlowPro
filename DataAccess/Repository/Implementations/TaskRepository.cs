using DataAccess.Context;
using Data.Model;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        /// Initializes a new instance of the TaskRepository class with the specified database context.
        /// </summary>
        /// <param name="dbContext">The database context used for task data access operations.</param>
        public TaskRepository(TaskdbContext dbContext)   
        {
            _Context = dbContext;
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
            return await _Context.EmpTask.FindAsync(id);
        }

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetAllTask()
        {
            return await _Context.EmpTask.ToListAsync();
        }

        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        public async Task<IEnumerable<ModelTask>> GetCompletedTasksAsync()
        {
            return await _Context.EmpTask.ToListAsync();
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
            await _Context.AddAsync(entity);
            await _Context.SaveChangesAsync();
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
            _Context.Update(entity);
            await _Context.SaveChangesAsync();
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
            var entity = await GetByIdAsync(id);
            if(entity !=null)
            {
                _Context.Remove(entity);
                await _Context.SaveChangesAsync();
            }
        }

    }
}

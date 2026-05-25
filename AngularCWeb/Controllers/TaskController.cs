using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;

namespace AngularCWeb.Controllers
{
#nullable disable

    /// <summary>
    /// Controller responsible for handling task-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        /// <summary>
        /// Service instance used for task-related business operations.
        /// </summary>
        private readonly ITaskService taskService;

        /// <summary>
        /// Logger instance used for application logging.
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the TaskController class with the specified task service.
        /// </summary>
        /// <param name="taskService">The service used for task-related operations.</param>
        public TaskController(ITaskService taskService)
        {
            this.taskService = taskService;
          
        }

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelTask>>> GetTask()
        {
            //this.logger.LogInformation("Started {Controller} - {Method}", nameof(TaskController), nameof(GetTask));
            var tk = await taskService.GetAllTaskAsync();
            return Ok(tk);
        }

        /// <summary>
        /// Retrieves a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>
        /// Returns the task details if found; otherwise, returns a not found response.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelTask>> GetAllTask(Guid id)
        {
            //this.logger.LogInformation("Started {Controller} - {Method} With TaskId: ", nameof(TaskController), nameof(GetTask), id);

            var task = await taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            //this.logger.LogInformation("Ended {Controller} - {Method}", nameof(TaskController), nameof(GetTask));
            return Ok(task);
        }

        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        [HttpGet("completed")]
        public async Task<ActionResult<IEnumerable<Task>>> GetCompletedTasks()
        {
            var tasks = await taskService.GetCompletedTkAsync();
            return Ok(tasks);
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="obj">The task object to be created.</param>
        /// <returns>
        /// Returns the created task with a success response.
        /// </returns>
        [HttpPost]
        public async Task<ActionResult<ModelTask>> CreateTask(ModelTask obj)
        {
            await taskService.CreateTaskAsync(obj);
            return CreatedAtAction(nameof(GetAllTask), new {id =obj.TaskId}, obj);
        }

        /// <summary>
        /// Updates an existing task.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <param name="task">The task object containing updated details.</param>
        /// <returns>
        /// Returns no content if the update is successful; otherwise, returns a bad request response.
        /// </returns>
        [HttpPut("{id}")]
        //[ProducesResponseType(typeof())]
        public async Task<IActionResult> UpdateTask(Guid id, ModelTask task)
        {
            if (id != task.TaskId)
                return BadRequest();
            await taskService.UpdateTaskAsync(task);
            return NoContent();
        }

        /// <summary>
        /// Deletes a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to be deleted.</param>
        /// <returns>
        /// Returns no content if the deletion is successful.
        /// </returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await taskService.DeleteTaskAsync(id);
            return NoContent();
        }

    }
}

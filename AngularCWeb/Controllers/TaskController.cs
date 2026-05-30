using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using System.Threading.Tasks;

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
        private readonly ILogger<TaskController> logger;

        /// <summary>
        /// Initializes a new instance of the TaskController class with the specified task service.
        /// </summary>
        /// <param name="taskService">The service used for task-related operations.
        /// </param>
        /// <param name="logger">
        /// Logger used to record controller activities, errors, and diagnostic information.
        /// </param>
        public TaskController(ITaskService taskService,ILogger<TaskController> logger)
        {
            this.taskService = taskService;
            this.logger = logger;
          
        }

        /// <summary>
        /// Retrieves all tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of all task records.
        /// </returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ModelTask>>> GetTask(string role)
        {
            this.logger.LogInformation("Started {Controller} - {Method}",nameof(TaskController),nameof(GetTask));
            var tk = await taskService.GetAllTaskAsync(role);
            this.logger.LogInformation("Ended {Controller} - {Method}", nameof(TaskController), nameof(GetTask));
            return Ok(tk);
        }

        /// <summary>
        /// Retrieves a task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task.</param>
        /// <returns>
        /// Returns the task details if found; otherwise, returns a not found response.
        /// </returns>
        [HttpGet("GetById/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelTask>> GetAllTask(Guid id)
        {
            this.logger.LogInformation("Started {Controller} - {Method}", nameof(TaskController), nameof(GetTask));
            var task = await taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            this.logger.LogInformation("Ended {Controller} - {Method}", nameof(TaskController), nameof(GetTask));
            return Ok(task);
        }

        /// <summary>
        /// Retrieves all completed tasks asynchronously.
        /// </summary>
        /// <returns>
        /// Returns a collection of completed task records.
        /// </returns>
        [HttpGet("completed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Task>>> GetCompletedTasks()
        {
            this.logger.LogInformation("Started {Controller} - {Method}",nameof(TaskController),nameof(GetCompletedTasks));
            var tasks = await taskService.GetCompletedTkAsync();
            this.logger.LogInformation("Ended {Controller} - {Method}",nameof(TaskController),nameof(GetCompletedTasks));
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ModelTask>> CreateTask(ModelTask obj)
        {
            this.logger.LogInformation("Started {Controller} - {Method}" ,nameof(TaskController),nameof(CreateTask));
            await taskService.CreateTaskAsync(obj);
            this.logger.LogInformation("Ended {Controller} - {Method}",nameof(TaskController),nameof(CreateTask));
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTask(Guid id, ModelTask task)
        {
            this.logger.LogInformation("Started {Controller} - {Method}",nameof(TaskController),nameof(UpdateTask));
            if (id != task.TaskId)
                return BadRequest();
            await taskService.UpdateTaskAsync(task);
            this.logger.LogInformation("Ended {Controller} - {Method}",nameof(TaskController),nameof(UpdateTask));
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            this.logger.LogInformation("Started {Controller} - {Method}",nameof(TaskController),nameof(DeleteTask));
            await taskService.DeleteTaskAsync(id);
            this.logger.LogInformation("Ended {Controller} - {Method}",nameof(TaskController),nameof(DeleteTask));
            return NoContent();
        }

        /// <summary>
        /// Retrieves tasks based on the specified user role.
        /// </summary>
        /// <param name="role">
        /// Role of the user (e.g., Admin or Developer).
        /// </param>
        /// <returns>
        /// Returns a list of tasks associated with the specified role.
        /// </returns>
        [HttpGet("role/{role}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTasksByRole(string role)
        {
            this.logger.LogInformation("Started {Controller} - {Method}",nameof(TaskController),nameof(GetTasksByRole));
            var tasks = await taskService.GetTasksByRoleAsync(role);
            this.logger.LogInformation("Ended {Controller} - {Method}",nameof(TaskController),nameof(GetTasksByRole));
            return Ok(tasks);
        }
    }
}

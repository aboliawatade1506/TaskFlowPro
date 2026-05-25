using System.ComponentModel.DataAnnotations;
namespace Data.Model
{
#nullable disable

    /// <summary>
    /// Represents a task entity in the application.
    /// </summary>
    public class ModelTask
    {
        /// <summary>
        /// Gets or sets the unique identifier for the task.
        /// </summary>
        [Key]
        public Guid TaskId { get; set; }
        //public Guid TaskId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets the name of the task.
        /// </summary>
        [Required]
        public string TaskName { get; set; }

        /// <summary>
        /// Gets or sets the description of the task.
        /// </summary>
        [Required]
        public string TaskDescription { get; set; }

        /// <summary>
        /// Gets or sets the current status of the task.
        /// </summary>
        [Required]
        public string TaskStatus { get; set; }

        /// <summary>
        /// Gets or sets the priority level of the task.
        /// </summary>
        [Required]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who assigned the task.
        /// </summary>
        [Required]
        public string AssignBy { get; set; }

        /// <summary>
        /// Gets or sets the due date of the task.
        /// </summary>
        [Required]
        public DateTime? DueDate { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Data.Model;

namespace DataAccess.Context
{
    /// <summary>
    /// Database context for managing task and user entities in the application.
    /// </summary>
    public class TaskdbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the TaskdbContext class with the specified options.
        /// </summary>
        /// <param name="options">The options to configure the database context.</param>
        public TaskdbContext(DbContextOptions<TaskdbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the task entity collection.
        /// </summary>
        public DbSet<ModelTask> EmpTask { get; set; }

        /// <summary>
        /// Gets or sets the user entity collection.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures the entity mappings and database schema for the application models.
        /// </summary>
        /// <param name="modelBuilder">The builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// <summary>
            /// task detaiils
            /// </summary>
            modelBuilder.Entity<ModelTask>().ToTable("EmpTask");
            modelBuilder.Entity<ModelTask>().Property(x => x.TaskId).HasDefaultValueSql("NEWSEQUENTIALID()");
            modelBuilder.Entity<ModelTask>().Property(t => t.TaskName).IsRequired().HasMaxLength(300);
            modelBuilder.Entity<ModelTask>().Property(t => t.TaskStatus).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<ModelTask>().Property(t => t.TaskDescription).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<ModelTask>().Property(t => t.Priority).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<ModelTask>().Property(t => t.AssignBy).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<ModelTask>().Property(t => t.DueDate).IsRequired();

            /// <summary>
            ///  user details
            /// </summary>
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.FullName).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.Role).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.ProfileImage).HasMaxLength(500);
        }
    }
}

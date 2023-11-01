using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Entities
{
    public class MnccContext: DbContext
    {
        public DbSet<WorkflowInstance> WorkflowInstances { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("mncc");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

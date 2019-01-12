using Microsoft.EntityFrameworkCore;
using IMDG_TaskListApp.Models;

namespace IMDG_TaskListApp.Repos
{
    public class TaskListContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        public DbSet<TaskItem> TaskItems {get;set;}
        public DbSet<TaskList> TaskLists {get;set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TaskList>(e=>{
                e.HasKey(x=>x.TaskId);
                e.HasMany(x=>x.Items);
                
                e.Property(x=>x.Name)
                    .HasMaxLength(50);
                
            });

            builder.Entity<TaskItem>(e=>{
                e.HasKey(x=>x.ItemId);
                e.HasOne(x=>x.TaskList)
                    .WithMany(x=>x.Items)
                    .HasForeignKey(x=>x.TaskId);

                e.Property(x=>x.Description)
                    .HasMaxLength(50);
            });
        }
    }
}
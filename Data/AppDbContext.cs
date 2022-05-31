using Microsoft.EntityFrameworkCore;
using TodoCustomList.Models;

namespace TodoCustomList.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder model)
        {

            model.Entity<TodoModel>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            model.Entity<UserModel>().Property(x => x.Id).HasDefaultValueSql("NewId()");
            model.Entity<TaskTodoModel>().Property(x => x.Id).HasDefaultValueSql("NewId()");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-UIB0ODE\\SQLEXPRESS;Database=TODO_API_SQL;Integrated Security=sspi;");

        public DbSet<TodoModel> Todos { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskTodoModel> Tasks { get; set; }
    }
}

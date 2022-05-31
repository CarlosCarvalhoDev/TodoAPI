using Microsoft.EntityFrameworkCore;
using TodoCustomList.Data;
using TodoCustomList.Models;
using TodoCustomList.Models.TaskTodo.TaskTodoDTO;
using TodoCustomList.Models.TaskTodo.TaskTodoVM;

namespace TodoCustomList.Services
{
    public class TaskTodoService
    {
        private readonly AppDbContext context = new AppDbContext();
        public async Task<TaskTodoModel> Create(CreateTaskTodoDTO task)
        {
            var newtask = new TaskTodoModel()
            {
                Name = task.Name,
                IsCompleted = task.IsCompleted,
                TodoId = task.TodoId
            };
            
            await context.Tasks.AddAsync(newtask);
            await context.SaveChangesAsync();

            return newtask;
        }

        public async Task<List<TaskTodoModel>> GetAll()
        {
            return await context.Tasks.AsQueryable().ToListAsync();
        }

        public async Task<TaskTodoModel> GetById(Guid id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task is null) throw new Exception();

            return task;
        }

        public async Task Delete(Guid id)
        {
            var task = await context.Tasks.FindAsync(id);

            if (task is null) throw new Exception();

            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
        }

        public async Task<TaskTodoModel> Update(UpdateTaskTodoDTO task)
        {
            var oldTask = await context.Tasks.FindAsync(task.Id);
            if (oldTask is null) throw new Exception();

            oldTask.IsCompleted = task.IsCompleted;
            oldTask.TodoId = task.TodoId;
            oldTask.Name = task.Name;

            context.Tasks.Update(oldTask);
            await context.SaveChangesAsync();

            return oldTask;
        }
    }
}

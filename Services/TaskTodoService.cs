using Microsoft.EntityFrameworkCore;
using TodoCustomList.Data;
using TodoCustomList.Models;
using TodoCustomList.Models.TaskTodo.Dto;
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
                Name = task.TaskTitle,
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

        public async Task<TaskTodoModel> Update(UpdateTaskTodoDTO taskDto)
        {
            var task = await context.Tasks.FindAsync(taskDto.Id);
            if (task is null) throw new Exception();

            task.IsCompleted = task.IsCompleted;
            task.TodoId = task.TodoId;
            task.Name = task.Name;

            context.Tasks.Update(task);
            await context.SaveChangesAsync();

            return task;
        }

        public async Task<List<TaskTodoModel>> GetAllTaskByTodoId(Guid id)
        {
            return await context.Tasks.AsQueryable().Where(a => a.TodoId == id).ToListAsync();
        }

        public async Task UpdateStatus(UpdateStatusTaskDTO taskDTO)
        {
            var task = await context.Tasks.FindAsync(Guid.Parse(taskDTO.TaskId));
            if(task is null) throw new Exception();

            task.IsCompleted = taskDTO.IsCompleted;

            context.Tasks.Update(task);
            await context.SaveChangesAsync();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using TodoCustomList.Data;
using TodoCustomList.Models;
using TodoCustomList.Models.Todo.Dto;

namespace TodoCustomList.Services
{
    public class TodoService
    {
        private readonly AppDbContext context = new AppDbContext();
        public async Task<TodoModel> Create(CreateTodoDTO createTodoDTO)
        {
            var todo = new TodoModel()
            {
                Title = createTodoDTO.Title,
                Description = createTodoDTO.Description,
                UserId = Guid.Parse(createTodoDTO.UserId),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();

            return todo;
        }

        public async Task<List<TodoModel>> GetAll()
        {
           return await context.Todos.AsQueryable().ToListAsync();
        }

        public async Task<TodoModel> GetById(Guid id)
        {
            var todo = await context.Todos.FindAsync(id);

            if (todo is null) throw new Exception("Nenhum registro encontrado");
            
            return todo;
        }

        public async Task Delete(Guid id)
        {
            var todo = await context.Todos.FindAsync(id);

            if (todo is null) throw new Exception();

            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
        }

        public async Task<TodoModel> Update(UpdateTodoDTO todo)
        {
            var oldTodo = await context.Todos.FindAsync(todo.Id);
            
            if (oldTodo is null) throw new Exception();

            oldTodo.Title = todo.Title;
            oldTodo.Description = todo.Description;
            oldTodo.UpdatedDate = DateTime.Now;
            oldTodo.UserId = todo.UserId;

            context.Todos.Update(oldTodo);
            await context.SaveChangesAsync();

            return oldTodo;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TodoCustomList.Data;
using TodoCustomList.Models;
using TodoCustomList.Models.Todo.Dto;
using TodoCustomList.Models.Todo.TodoVM;

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

        public async Task<IEnumerable<TodoModel>> GetAll()
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

        public async Task<TodoModel> Update(string todoId,  UpdateTodoDTO updatedTodo)
        {
            var Todo = await context.Todos.FindAsync(Guid.Parse(todoId));
            
            if (Todo is null) throw new Exception();

            Todo.Title = updatedTodo.Title;
            Todo.Description = updatedTodo.Description;
            Todo.UpdatedDate = DateTime.Now;
            Todo.UserId = Todo.UserId;

            context.Todos.Update(Todo);
            await context.SaveChangesAsync();

            return Todo;
        }

        public async Task<TodoModel> ChangeTodoUser(string todoId, string userId)
        {
            var oldTodo = await context.Todos.FindAsync(Guid.Parse(todoId));
            if(oldTodo is null) throw new Exception("Nenhum Todo encontrado para este parametro.");

            var user = await context.Users.FindAsync(Guid.Parse(userId));
            if (user is null) throw new Exception("Nao existe usuario registrado para o ID informado");

            oldTodo.UserId = user.Id;


            context.Todos.Update(oldTodo);
            await context.SaveChangesAsync();

            return oldTodo;
        }


        public List<TodoSumaryResponseViewModel> GetAllUserTodo()
        {
            var query = from user in context.Users
                        join todo in context.Todos on user.Id equals todo.UserId into userTodo
                        from result in userTodo.DefaultIfEmpty()
                        select new TodoSumaryResponseViewModel
                        {
                            TodoId = result.Id.ToString(),
                            Title = result.Title,
                            UserId = user.Id.ToString(),
                        };

            return query.ToList();
        }


    }
}

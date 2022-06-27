using Microsoft.EntityFrameworkCore;
using TodoCustomList.Data;
using TodoCustomList.Models;
using TodoCustomList.Models.Todo.TodoVM;
using TodoCustomList.Models.User.Dto;

namespace TodoCustomList.Services
{
    public class UserService
    {
        private readonly AppDbContext context = new AppDbContext();
        public async Task<UserModel> CreateUser(CreateUserDTO userDTO)
        {
            var user = new UserModel()
            {
                Email = userDTO.Email,
                Password = userDTO.Password,
                Name = userDTO.Name
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            
            return user;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await context.Users.AsQueryable().ToListAsync();
        }

        public async Task<UserModel> GetById(Guid id)
        {
            var user = await context.Users.FindAsync(id);

            if (user is null) throw new Exception(); 
            
            return user;
        }

        public async Task Delete(Guid id)
        {
            var user = await context.Users.FindAsync(id);

            if (user is null) throw new Exception();

            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<UserModel> Update(UserModel user)
        {
            var oldUser = await context.Users.FindAsync(user.Id);
            if (oldUser is null) throw new Exception();
           
            oldUser.Email = user.Email;
            oldUser.Password = user.Password;
            oldUser.Name = user.Name;

            context.Users.Update(oldUser);
            await context.SaveChangesAsync();

            return oldUser;
        }

        public async Task<List<TodoSumaryResponseViewModel>> GetAllUserTodo()
        {
            var query = await context.Users.AsQueryable()
                       .GroupJoin(context.Todos,
                            user => user.Id,
                            todo => todo.UserId,
                            (user, todo) => new { User = user, Todo = todo.DefaultIfEmpty() })
                       .SelectMany(result => result.User.Id,
                                    (result, todo) => new TodoSumaryResponseViewModel
                                    {
                                        UserId = result.User.Id,
                                        Id = result.Todo.user,
                                        Title = result.Title,

                                    }).ToListAsync();
            

        }




    }
}

using Microsoft.EntityFrameworkCore;
using TodoCustomList.Data;
using TodoCustomList.Models;
using TodoCustomList.Models.User.Dto;

namespace TodoCustomList.Services
{
    public class UserService
    {
        private readonly AppDbContext context = new AppDbContext();

        #region METHOD CREATE USER
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
        #endregion

        #region Get All
        public async Task<List<UserModel>> GetAll()
        {
            return await context.Users.AsQueryable().ToListAsync();
        }
        #endregion

        #region Get By Id
        public async Task<UserModel> GetById(Guid id)
        {
            var user = await context.Users.FindAsync(id);

            if (user is null) throw new Exception();

            return user;
        }
        #endregion

        #region Delete
        public async Task Delete(Guid id)
        {
            var user = await context.Users.FindAsync(id);

            if (user is null) throw new Exception();

            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
        #endregion

        #region Update
        public async Task<UserModel> Update(UserModel user)
        {
            var oldUser = await context.Users.FindAsync(user.Id);
            if (oldUser is null) throw new Exception("Erro ao atualizar usuario");

            oldUser.Email = user.Email;
            oldUser.Password = user.Password;
            oldUser.Name = user.Name;

            context.Users.Update(oldUser);
            await context.SaveChangesAsync();

            return oldUser;
        }
        #endregion

    }
}

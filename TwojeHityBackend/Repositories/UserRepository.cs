using TwojeHity.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using TwojeHity.Models;

namespace TwojeHity.Repositories
{

    public interface IUserRepository
    {
        Task<int> RegisterUser(User newUser);

        Task<User> GetUserByLogin(string login);

        Task<bool> LoginExist(User user);
        Task<User> GetUserById(int id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterUser(User newUser)
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByLogin(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task<bool> LoginExist(User user)
        {
            return await _context.Users.AnyAsync(x => x.Login == user.Login);
        }
    }
}
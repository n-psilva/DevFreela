﻿using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == id); ;
            return user;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string passwordHash)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Email == email && user.Password == passwordHash);
        }
    }
}

using DataAccess.DbContexts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRepository : IUserRepository
    {
        WebShopDbContext _dbContext;

        public UserRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(User => User.Email == email);
        }
    }
}

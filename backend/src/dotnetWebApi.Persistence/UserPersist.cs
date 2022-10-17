using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Domain.Identity;
using dotnetWebApi.Persistence.Contextos;
using dotnetWebApi.Persistence.Contratos;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly dotnetWebAPIContext context;
        public UserPersist(dotnetWebAPIContext context): base (context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await this.context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await this.context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await this.context.Users.SingleOrDefaultAsync(user => user.UserName.ToLower() == userName.ToLower());
        }

    }
}
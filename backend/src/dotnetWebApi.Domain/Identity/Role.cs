using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace dotnetWebApi.Domain.Identity
{
    public class Role: IdentityRole<int>
    {
        public List<UserRole> UserRoles {get; set;}
    }
}
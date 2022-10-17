using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetWebApi.Domain.Enum;
using Microsoft.AspNetCore.Identity;

namespace dotnetWebApi.Domain.Identity
{
    // utilizar int apenas para desenvolvimento, em produção usar Guid Uuid   
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string description { get; set; }
        public Degree Degree { get; set; }
        public Occupation occupation { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}
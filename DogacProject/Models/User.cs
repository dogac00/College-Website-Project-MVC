using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogacProject.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public long UserId { get; set; }
        public string City { get; set; }
    }
}

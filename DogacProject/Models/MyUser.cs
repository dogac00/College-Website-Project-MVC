using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogacProject.Models
{
    public class MyUser : IdentityUser
    {
        public string Name { get; set; }
        public long UserIdNumber { get; set; }
        public string City { get; set; }
        public int departmentManagerId { get; set; } = 0;
    }
}

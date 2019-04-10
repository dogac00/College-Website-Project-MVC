using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogacProject.Models
{
    public class DogacContext : IdentityDbContext
    {
        public DogacContext(DbContextOptions<DogacContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DogacProject.Models
{
    public class DogacContext : DbContext
    {
        public DogacContext(DbContextOptions<DogacContext> options) : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }

        public DbSet<DogacProject.Models.Department> Department { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogacProject.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long StuID { get; set; }
        public string Department { get; set; }
    }
}

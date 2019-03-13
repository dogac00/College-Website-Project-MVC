using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DogacProject.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "Student Name (Required)")]
        [Required(ErrorMessage = "Student Name is Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Student name must between 3 and 100 characters.")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }

        [Display(Name = "Student ID (Required)")]
        [Required(ErrorMessage = "Student ID is Required")]
        public long StuID { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }
    }
}

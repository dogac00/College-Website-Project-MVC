using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DogacProject.Models;

namespace DogacProject.Controllers
{
    public class StudentsController : Controller
    {
        DogacContext DogacContext;

        public StudentsController(DogacContext context)
        {
            DogacContext = context;
        }

        public IActionResult Index()
        {
            var students = DogacContext.Students.ToList();
            return View(students);
        }

        public IActionResult Detay(int id)
        {

            Student student = DogacContext.Students.Where(s => s.Id == id).FirstOrDefault();
            if (student != null)
            {
                return View(student);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
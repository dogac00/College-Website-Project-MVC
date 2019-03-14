using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DogacProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Create()
        {
            ViewData["DepartmentName"] = new SelectList(DogacContext.Department, "Id", "Name", -1);
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                DogacContext.Students.Add(stu);
                DogacContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(stu);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var student = DogacContext.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);


        }
        [HttpPost]
        public IActionResult Edit(int? id, Student student)
        {

            if (!id.HasValue)
            {
                return BadRequest();
            }

            if (student == null)
            {
                return NotFound();
            }

            if (id != student.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                DogacContext.Students.Update(student);
                DogacContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }
        }

        public IActionResult Detay(int id)
        {

            Student student = DogacContext.Students.Include(s=>s.Department).Where(s => s.Id == id).FirstOrDefault();
            if (student != null)
            {
                return View(student);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await DogacContext.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await DogacContext.Students.FindAsync(id);
            DogacContext.Students.Remove(department);
            await DogacContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return DogacContext.Students.Any(e => e.Id == id);
        }
    }
}
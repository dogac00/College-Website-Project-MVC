using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DogacProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DogacProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class StudentsController : Controller
    {
        DogacContext DogacContext;
        UserManager<MyUser> _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public StudentsController(DogacContext context, UserManager<MyUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            DogacContext = context;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student stu, IFormFile FileUrl)
        {
            stu.dateCreated = DateTime.Now;

            if (ModelState.IsValid)
            {
                string dirPath = Path.Combine(_hostingEnvironment.WebRootPath, @"uploads\");
                var fileName = Guid.NewGuid().ToString().Replace("-", "") + "_" + FileUrl.FileName;

                using (var fileStream = new FileStream(dirPath + Path.GetFileName(fileName), FileMode.Create))
                {
                    await FileUrl.CopyToAsync(fileStream);
                }

                stu.ImageUrl = Path.GetFileName(fileName);
                DogacContext.Add(stu);

                await DogacContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DepartmentId"] = new SelectList(DogacContext.Department, "Id", "Name", stu.DepartmentId);
            return View(stu);
        }

        public IActionResult Edit(int? id)
        {
            ViewData["DepartmentName"] = new SelectList(DogacContext.Department, "Id", "Name", -1);

            if (!id.HasValue)
            {
                var students = DogacContext.Students.ToList();
                return View(students);
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

            if (User.IsInRole("departmentManager" + student.DepartmentId.ToString()))
            {
                return Unauthorized();
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

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            ViewData["ManagerToId"] = user.departmentManagerId;

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
            if (User.IsInRole("departmentManager" + id.ToString()))
            {
                return Unauthorized();
            }

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
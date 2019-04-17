using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogacProject.Models;
using DogacProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DogacProject.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly DogacContext _context;
        private readonly UserManager<MyUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(DogacContext context, UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: UserManagement
        public async Task<ActionResult> Index()
        {
            var userList = _context
                .Users
                .ToList();

            List<UserModel> userModelList = new List<UserModel>();

            foreach (var item in userList)
            {
                bool isadmin = await _userManager.IsInRoleAsync(item, "admin");
                bool isDepartmentManager = await _userManager.IsInRoleAsync(item, "departmentManager");

                var user = new UserModel
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    UserIdNumber = item.UserIdNumber,
                    IsAdmin = isadmin,
                    IsDepartmentManager = isDepartmentManager
                };

                userModelList.Add(user);
            }

            return View(userModelList);
        }


        public async Task<ActionResult> MakeAdmin(string id)
        {
            if (!(await _roleManager.RoleExistsAsync("admin")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            }
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveAdmin(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "admin");
            return RedirectToAction("index");
        }


        public async Task<ActionResult> MakeDepartmentManager(string id)
        {
            if (!(await _roleManager.RoleExistsAsync("departmentManager")))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "departmentManager" });
            }

            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, "departmentManager");

            return RedirectToAction("index");
        }

        public async Task<ActionResult> RemoveDepartmentManager(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(user, "departmentManager");

            return RedirectToAction("index");
        }
    }
}
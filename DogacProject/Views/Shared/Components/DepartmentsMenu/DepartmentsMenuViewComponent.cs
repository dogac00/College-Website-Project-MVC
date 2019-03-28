using DogacProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CetBlog.Views.Shared.Components.DepartmentsMenu
{
    public class DepartmentsMenuViewComponent : ViewComponent
    {
        private readonly DogacContext dbContext;

        public DepartmentsMenuViewComponent(DogacContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await dbContext.Department.ToListAsync();
            return View(categories);
        }
    }
}

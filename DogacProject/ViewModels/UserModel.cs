using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogacProject.ViewModels
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public long UserIdNumber { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDepartmentManager { get; set; }
        public int ManagerTo { get; set; }
    }
}

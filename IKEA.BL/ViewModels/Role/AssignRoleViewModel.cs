using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace IKEA.PL.ViewModels.Role
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}

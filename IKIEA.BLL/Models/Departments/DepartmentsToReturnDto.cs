using IKIEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Models.Departments
{
    public class DepartmentsToReturnDto 
    {
        public int Id { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int LastModifiedBy { get; set; }
        //public DateTime LastModifiedOn { get; set; }
        public string Name { get; set; } = null;
        public string Code { get; set; } = null;
        public string Description { get; set; } = null;
        public DateTime CreationDate { get; set; }

        public static explicit operator DepartmentsToReturnDto (Department department)
        {
            return new DepartmentsToReturnDto
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate,
                Description = department.Description,
            };
        }
    }
}

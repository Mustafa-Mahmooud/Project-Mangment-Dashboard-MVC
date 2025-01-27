using IKIEA.BLL.Models.Departments;
using IKIEA.DAL.Data.Migrations;
using IKIEA.DAL.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IKIEA.BLL.Services.Departments
{
    public interface IDepartmentService

    {
        Task<IEnumerable<DepartmentsToReturnDto>> GetAllDepartmentsAsync( );
        Task<DepartmentsToReturnDto?> GetDepartmentByIdAsync(int Id);
        Task<int> CreateDepartmentAsync(CreateDepartmentDto department);
        Task<int> UpdateDepartmentAsync(UpdatedDepartmentId department); 
        Task<bool> DeleteDepartmentAsync(int departmentId);
        public IEnumerable<DepartmentsToReturnDto> GetDepartments(string search);




    }
}

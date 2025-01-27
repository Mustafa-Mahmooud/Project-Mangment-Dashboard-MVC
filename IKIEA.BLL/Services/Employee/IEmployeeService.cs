using IKIEA.BLL.Models.Departments;
using IKIEA.BLL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Services.Employee
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string search);
        Task<EmployeeToReturnDto? >GetEmployeeByIdAsync(int Id);
        Task<int> CreateEmployeeAsync(CreateEmployeeDto employee);
        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employee);
        Task<bool> DeleteEmployeeAsync(int employeeId);

    }
}

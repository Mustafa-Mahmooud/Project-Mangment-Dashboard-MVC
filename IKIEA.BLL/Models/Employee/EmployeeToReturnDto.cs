using IKIEA.BLL.Models.Departments;
using IKIEA.DAL.Common.Enums;
using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Models.Employess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Models.Employee
{
    public class EmployeeToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }

        public Department? Department { get; set; }

        public static explicit operator EmployeeToReturnDto(Employees employees)
        {
            return new EmployeeToReturnDto
            {
                EmployeeType = employees.EmployeeType,
                Name = employees.Name,
                Age = employees.Age,
                Address = employees.Address,
                PhoneNumber = employees.PhoneNumber,
                Email = employees.Email,
                IsActive = employees.IsActive,
                Gender = employees.Gender,
                HiringDate = employees.HiringDate,
                Salary = employees.Salary,
               


            };
        }
    }
}

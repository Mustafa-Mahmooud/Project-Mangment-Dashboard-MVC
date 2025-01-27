using IKIEA.BLL.Models.Employee;
using IKIEA.DAL.Models.Employess;
using IKIEA.DAL.Repositories.EmployeeRepository;
using IKIEA.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {

       
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
        {
            var employee = new Employees()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedOn = DateTime.Now,
                LastModifiedOn = DateTime.UtcNow,
                IsActive = employeeDto.IsActive,
                LastModifiedBy = 1,
                CreatedBy = 1,



            };
             _unitOfWork.employeeRepository.Add(employee);
            return await _unitOfWork.CompleteAsync();


        }
        public async Task <bool> DeleteEmployeeAsync(int employeeId)
        {

            var employeeRepo = _unitOfWork.employeeRepository;
            var employee = await employeeRepo.GetByIdAsync(employeeId);
            if(employee is { })
            {
                employeeRepo.Delete(employee) ;

            }
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public async Task< IEnumerable<EmployeeToReturnDto>> GetAllEmployeesAsync(string search)
        {
            var emplpyees = await _unitOfWork.
                employeeRepository
                .GetAllAsIQueryable()
                .Where(employeeDto => string.IsNullOrEmpty(search) || employeeDto.Name.Contains(search))
                .Select(employeeDto => new EmployeeToReturnDto
                {
                    Id = employeeDto.Id, // send id to achieve model binding
                    Name = employeeDto.Name,
                    Age = employeeDto.Age,
                    Address = employeeDto.Address,
                    Salary = employeeDto.Salary,
                    Email = employeeDto.Email,
                    PhoneNumber = employeeDto.PhoneNumber,
                    HiringDate = employeeDto.HiringDate,
                    Gender = employeeDto.Gender,
                    EmployeeType = employeeDto.EmployeeType,
                    Department = employeeDto.department,
                }).ToListAsync();
            return emplpyees;
        }
        public async Task< EmployeeToReturnDto?> GetEmployeeByIdAsync(int Id)
        {
            var employee = await _unitOfWork.employeeRepository.GetByIdAsync(Id);
            if(employee is { })
            return new EmployeeToReturnDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Salary = employee.Salary,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                HiringDate= employee.HiringDate,
                IsActive = employee.IsActive,
            };
            return null;
        }
        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto)
        {

            var employee = new Employees()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.departmentId,
                CreatedOn = DateTime.Now,
                LastModifiedOn = DateTime.UtcNow,
                IsActive = employeeDto.IsActive,
                LastModifiedBy = 1,
                CreatedBy = 1,
                IsDeleted = false,

                



            };
           _unitOfWork.employeeRepository.Update(employee);
            return await _unitOfWork.CompleteAsync();
        }
    }
}

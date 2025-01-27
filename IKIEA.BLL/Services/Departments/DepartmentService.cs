using IKIEA.BLL.Models.Departments;
using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Repositories._Generic;
using IKIEA.DAL.Repositories.DepartmentRepository;
using IKIEA.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork) //dependency injection
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CreateDepartmentAsync(CreateDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreatedOn = DateTime.Now,
                CreationDate = departmentDto.CreationDate,
                
                CreatedBy =1 ,
                LastModifiedBy =1 ,
                LastModifiedOn = DateTime.UtcNow,
            };
             _unitOfWork.departmentRepository.Add(department);
            return await _unitOfWork.CompleteAsync();

        }
        public async Task<bool> DeleteDepartmentAsync(int departmentId)
        {
            var departmentRepoo =  _unitOfWork.departmentRepository;
            var department = await departmentRepoo.GetByIdAsync(departmentId);
            if (department != null) 
                 departmentRepoo.Delete(department) ;
            return await _unitOfWork.CompleteAsync() > 0;
        }
        public  async Task<IEnumerable<DepartmentsToReturnDto>> GetAllDepartmentsAsync(  )
        {
            var departments = await _unitOfWork.departmentRepository.GetAllAsIQueryable()
              
                .Select(d => new DepartmentsToReturnDto()
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                Description = d.Description,
                CreationDate = d.CreationDate,

            }).AsNoTracking().ToListAsync();
            return  departments;
            
           
        }
        public  IEnumerable<DepartmentsToReturnDto> GetDepartments(string search)
        {
             return  _unitOfWork.departmentRepository.GetAllAsIQueryable()
               .Where(d => string.IsNullOrEmpty(search) || d.Name.Contains(search))
                .Select(d => new DepartmentsToReturnDto()
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    Description = d.Description,
                    CreationDate = d.CreationDate,

                }).AsNoTracking().ToList();
            


        }
        public async Task<DepartmentsToReturnDto?> GetDepartmentByIdAsync(int Id)
        {
            var department = await _unitOfWork.departmentRepository.GetByIdAsync(Id);
            if (department is not null)
            return new DepartmentsToReturnDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                

            };

            return null;
        }
        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentId departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,              
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
             _unitOfWork.departmentRepository.Update(department);
            return await _unitOfWork.CompleteAsync();
        }

       
    }
}

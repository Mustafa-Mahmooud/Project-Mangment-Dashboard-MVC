using IKIEA.DAL.Data;
using IKIEA.DAL.Repositories._Generic;
using IKIEA.DAL.Repositories.DepartmentRepository;
using IKIEA.DAL.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IEmployeeRepository employeeRepository { 
            get 
            {
                return new EmployeeRepository(_appDbContext);
            }
        }
        public IDepartmentRepository departmentRepository {
            get 
            {
                return new DepartmentRepository(_appDbContext);
            }
                }

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            
        }
        public async Task <int> CompleteAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync() 
        {
            _appDbContext.DisposeAsync();
        }

        
    }
}

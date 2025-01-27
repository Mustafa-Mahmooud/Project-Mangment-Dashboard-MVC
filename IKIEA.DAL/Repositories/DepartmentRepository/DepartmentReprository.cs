using IKIEA.DAL.Data;
using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Models.Employess;
using IKIEA.DAL.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly AppDbContext appDbContext;

        public DepartmentRepository(AppDbContext dbcontext)
        {
            appDbContext = dbcontext;       
        }
        public async Task< IEnumerable<Department>> GetAllAsync(bool WithNoTracking = true)
        {
            if (WithNoTracking) return  await appDbContext.Departments.AsNoTracking().ToListAsync();
            return await appDbContext.Departments.ToListAsync();
        }
        public async Task<Department> GetByIdAsync(int id)
        {
            return await appDbContext.Departments.FindAsync(id);
        }

        public void Add(Department department)
        {
            appDbContext.Add(department);
            
        }

        public void Update(Department department)
        {
            appDbContext.Update(department);
            
        }
        public void Delete(Department department)
        {
            appDbContext.Remove(department);
            

        }

        public IQueryable<Department> GetAllAsIQueryable()
        {
            return appDbContext.Departments;
        }

        
    }
}

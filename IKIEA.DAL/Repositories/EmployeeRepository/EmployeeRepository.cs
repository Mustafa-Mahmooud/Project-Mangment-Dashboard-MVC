using IKIEA.DAL.Data;
using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Models.Employess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext appDbContext;

        public EmployeeRepository(AppDbContext dbcontext)
        {
            appDbContext = dbcontext;
        }
        public async Task<IEnumerable<Employees>> GetAllAsync(bool WithNoTracking = true)
        {
            if (WithNoTracking)
            return await appDbContext.Employees.AsNoTracking().ToListAsync();
            return await appDbContext.Employees.ToListAsync();
        }      
        public async Task<Employees> GetByIdAsync(int id)
        {
            return await appDbContext.Employees.FindAsync(id);
        }

        public void Add(Employees? Employees)
        {
            appDbContext.Add(Employees);
           
        }

        public void Update(Employees Employees)
        {
            appDbContext.Update(Employees);
            
        }
       
        public IQueryable<Employees> GetAllAsIQueryable()
        {
            return appDbContext.Employees;
        }

        public void Delete(Employees employees)
        {
            appDbContext.Remove(employees);
           
        }
    }
}

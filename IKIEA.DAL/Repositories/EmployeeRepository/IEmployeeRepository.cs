using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Models.Employess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetAllAsync(bool WithNoTracking = true);
        IQueryable<Employees> GetAllAsIQueryable();
        Task<Employees> GetByIdAsync(int id);
        void Add(Employees employees);
        void Update(Employees employees);
        void Delete(Employees employees);
    }
}

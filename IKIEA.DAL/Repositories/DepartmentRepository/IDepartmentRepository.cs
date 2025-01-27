using IKIEA.DAL.Models;
using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Models.Employess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.Repositories._Generic
{
    public interface IDepartmentRepository
    { 
        Task<IEnumerable<Department>> GetAllAsync(bool WithNoTracking = true);
        IQueryable<Department> GetAllAsIQueryable();
        Task<Department> GetByIdAsync(int id);
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}

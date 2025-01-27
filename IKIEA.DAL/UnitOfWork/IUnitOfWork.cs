using IKIEA.DAL.Repositories._Generic;
using IKIEA.DAL.Repositories.EmployeeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IEmployeeRepository  employeeRepository { get;  }
        public IDepartmentRepository departmentRepository { get; }

        Task<int> CompleteAsync();
         
        


    }
}

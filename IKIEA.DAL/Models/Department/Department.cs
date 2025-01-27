using IKIEA.DAL.Models.Employess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.Models.Department
{
    public class Department : ModelBase
    {
        public string Name { get; set; } = null;
        public string Code { get; set; } = null;
        public string Description { get; set; } = null;
        public DateTime CreationDate { get; set; }

        public ICollection<Employees> employees { get; set; } = new HashSet<Employees>();


    }
}

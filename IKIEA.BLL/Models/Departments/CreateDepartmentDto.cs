using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Models.Departments
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Name Is Required :(")]
        public string Name { get; set; } = null;

        [Required(ErrorMessage = "Code Is Required :(")]
        public string Code { get; set; } = null;

        [Required(ErrorMessage = "Description Is Required :(")]

        public string Description { get; set; } = null;

        [Display(Name = "Date Of Creation "),Required(ErrorMessage ="Required")]
        public DateTime CreationDate { get; set; }

        
    }
}

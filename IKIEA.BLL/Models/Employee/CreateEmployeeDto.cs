using IKIEA.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.BLL.Models.Employee
{
    public class CreateEmployeeDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int? Age { get; set; }
        [Required]

        public string? Address { get; set; }    
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Phone Number ")]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [Required]

        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required]
        public string? Email { get; set; }

        [Display (Name = "Is Active"),Required]
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Hiring Date"),Required]
        public DateOnly HiringDate { get; set; }
        [Required]

        public Gender Gender { get; set; }
        [Required]

        public EmployeeType EmployeeType { get; set; }

        public int DepartmentId { get; set; }

        public string? Department { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKIEA.DAL.Models.Identity
{
	public class ApplicationUser :IdentityUser
	{
		public string Fname { get; set; } = null;
		public string Lname { get; set; } = null;
        public bool  IsActive { get; set; }


    }
}

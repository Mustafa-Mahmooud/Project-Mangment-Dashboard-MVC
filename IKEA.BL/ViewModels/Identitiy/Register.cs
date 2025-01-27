using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Identitiy
{
	public class Register
	{


		[Required]
        public string  Fname { get; set; }

		[Required]
        public string Lname { get; set; }

        [EmailAddress]
		[Required]
		public string Email { get; set; } = null;

		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; } = null;

		[DataType(DataType.Password)]
		[Compare("Password")]
		[Required]
		public string ConfirmPassword { get; set; } = null;
        public bool IAgree { get; set; }

    }
}

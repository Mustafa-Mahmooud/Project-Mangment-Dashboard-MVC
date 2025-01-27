using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{

		[EmailAddress]
		[Required]
		public string Email { get; set; } = null;

	}
}

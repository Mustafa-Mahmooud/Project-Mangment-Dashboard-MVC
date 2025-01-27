using IKEA.PL.ViewModels.Identitiy;
using IKEA.BL.Controllers;
using IKIEA.DAL.Common;
using IKIEA.DAL.Data;
using IKIEA.DAL.Models;
using IKIEA.DAL.Repositories;
using IKIEA.DAL.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IKEA.PL.ViewModels.Identitiy;
using IKIEA.DAL.Models.Identity;
using IKEA.PL.ViewModels.Account;
using Demo.PL.Helpers;

namespace IKEA.PL.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(Register model)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = model.Email.Split('@')[0],
					Email = model.Email,
					Fname = model.Fname,
					Lname = model.Lname,
					IsActive = model.IAgree
				};

				var result = await _userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					return RedirectToAction("Login");
				}

				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			return View(model);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
					if (result.Succeeded)
					{
						return RedirectToAction("Index", "Home");
					}

					ModelState.AddModelError(string.Empty, "Invalid login attempt.");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email doesn't exist.");
				}
			}

			return View(model);
		}

		public new async Task<IActionResult> SignOut()
		{
			 await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));

		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var Email = new Email()
					{
						Subject = "Reset Password",
						To = model.Email,
						Body = "Reset Password Link",

					};
					EmailSettings.SendEmail(Email);
					return RedirectToAction(nameof(CheckYourInbox));
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Email Doesn't Exist");
				
				}
			}
				return View("ForgetPassword", model);
			

		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
	}
}

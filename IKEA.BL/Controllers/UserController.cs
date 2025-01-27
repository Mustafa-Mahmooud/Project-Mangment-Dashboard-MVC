using IKEA.PL.ViewModels;
using IKIEA.DAL.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKEA.PL.Controllers
{
    
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search)
        {
            List<UserViewModel> users;

            if (string.IsNullOrEmpty(search))
            {
               
                var userList = await _userManager.Users.ToListAsync();

               
                users = new List<UserViewModel>();
                foreach (var user in userList)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    users.Add(new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.Fname,
                        LName = user.Lname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = roles
                    });
                }
            }
            else
            {
               
                var user = await _userManager.FindByNameAsync(search);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user); 
                    var mappedUser = new UserViewModel
                    {
                        Id = user.Id,
                        FName = user.Fname,
                        LName = user.Lname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = roles
                    };

                    return View(new List<UserViewModel> { mappedUser });
                }

               
                users = new List<UserViewModel>();
            }

            return View(users);
        }

        //public async Task<IActionResult> Update(string id)
        //{
        //    return await Details(id);
        //}
        //public async Task<IActionResult> Update(UserViewModel model, [FromRoute] string id)
        //{
        //    if (id != model.Id)
        //        return BadRequest();

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var user = await _userManager.FindByIdAsync(id);
        //            user.PhoneNumber = model.PhoneNumber;
        //            user.Fname = model.FName;
        //            user.Lname = model.LName;
        //            await _userManager.UpdateAsync(user);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, ex.Message);
        //        }
        //    }

        //    return View(model);
        //}

        // GET: User/Delete/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FName = user.Fname,
                LName = user.Lname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return View(userViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Map the user to a ViewModel if necessary
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                FName = user.Fname,
                LName = user.Lname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };

            return View(userViewModel); // Returns the user details for confirmation
        }

       
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View("Delete", new UserViewModel
            {
                Id = user.Id,
                FName = user.Fname,
                LName = user.Lname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }




    }
}

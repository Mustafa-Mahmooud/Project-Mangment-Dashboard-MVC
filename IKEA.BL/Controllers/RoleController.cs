using IKEA.PL.ViewModels;
using IKEA.PL.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace IKEA.PL.Controllers
{
    
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string search)
        {
            List<RoleViewModel> mappedRoles;

            // Fetch all roles
            var roles = await _roleManager.Roles.ToListAsync();

            // If a search term is provided, filter the roles by name
            if (!string.IsNullOrEmpty(search))
            {
                roles = roles.Where(role => role.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Map the filtered (or all) roles to the view model
            mappedRoles = roles.Select(role => new RoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            }).ToList();

            // Return the mapped roles to the view
            return View(mappedRoles);
        }


        public async Task<IActionResult> Create()
        {
            return  View();
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = new RoleViewModel
                {
                    Id = model.Id,
					RoleName = model.RoleName,
                   
                };

                await _roleManager.CreateAsync(new IdentityRole(MappedRole.RoleName));
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _roleManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var RoleViewModel = new RoleViewModel
            {

				RoleName = user.Name,
                
               
            };

            return View(RoleViewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _roleManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

           
            var RoleViewModel = new RoleViewModel
            {
                Id = user.Id,
				RoleName = user.Name,
                
            };

            return View(RoleViewModel); 
        }


        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = await _roleManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            try
            {
                var result = await _roleManager.DeleteAsync(user);
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

            return View("Delete", new RoleViewModel
            {
                Id = user.Id,
				RoleName = user.Name,
            });
        }

    }
}

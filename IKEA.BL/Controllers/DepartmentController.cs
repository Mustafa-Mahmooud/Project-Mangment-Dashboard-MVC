using IKEA.PL.ViewModels.Departments;
using IKIEA.BLL.Models.Departments;
using IKIEA.BLL.Services.Departments;
using IKIEA.DAL.Models.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> Logger, IWebHostEnvironment environment)
        {
            _logger = Logger;
            _environment = environment;
            _departmentService = departmentService;
        }

        #region Index

        [HttpGet]
        public IActionResult Index(string search)
        {
            var departments = _departmentService.GetDepartments(search);
            return View(departments);
        }

        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateDepartmentDto department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }
            var message = string.Empty;

            try
            {
                var result = await _departmentService.CreateDepartmentAsync(department);
                if (result > 0)
                {
                    TempData["Message"] = "Department Is Created :(";
                }

                else
                {

                    TempData["Message"] = "Department Isn't Created ;(";

                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Department Isn't Created :(";
                    return View("Error", message);

                }
            }
        }

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            else
                return View(department);
        }

        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400 Request

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();

            else
                return View(new DepartmentEditViewModel()
                {
                    Code = department.Code,
                    Name = department.Name,
                    CreationDate = department.CreationDate,
                    Description = department.Description,
                });
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, DepartmentEditViewModel departmentEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentEditViewModel);
            }

            var message = string.Empty;

            try
            {
                var departmentToUpdate = new UpdatedDepartmentId()
                {
                    Id = id,
                    Code = departmentEditViewModel.Code,
                    Name = departmentEditViewModel.Name,
                    Description = departmentEditViewModel.Description,
                    CreationDate = departmentEditViewModel.CreationDate,

                };

                var result = await _departmentService.UpdateDepartmentAsync(departmentToUpdate) > 0;
                if (result) return RedirectToAction(nameof(Index));

                message = "An Error Occurred In Updating The Department :(";
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred In Updating The Department :(";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentEditViewModel);



        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);
            if (department is null)
                return NotFound();

            return View(department);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {

                var Deleted = await _departmentService.DeleteDepartmentAsync(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                message = "An Error Occurred While Deleting :(";

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred In Deleting The Department :(";

            }
            return RedirectToAction(nameof(Index));


        }
        #endregion
    }
}


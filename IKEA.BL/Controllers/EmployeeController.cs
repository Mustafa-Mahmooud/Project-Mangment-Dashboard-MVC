using IKEA.PL.ViewModels.Departments;
using IKEA.PL.ViewModels.Employee;
using IKIEA.BLL.Models.Departments;
using IKIEA.BLL.Models.Employee;
using IKIEA.BLL.Services.Departments;
using IKIEA.BLL.Services.Employee;
using IKIEA.DAL.Models.Department;
using IKIEA.DAL.Repositories.DepartmentRepository;
using IKIEA.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger
                                , IWebHostEnvironment environment, IDepartmentService departmentService)
        {
            _logger = logger;
            _environment = environment;
            _departmentService = departmentService;
            _employeeService = employeeService;

        }

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {
            var employee = await _employeeService.GetAllEmployeesAsync(search);
            return View(employee);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["Departments"] = await _departmentService.GetAllDepartmentsAsync();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid) { return View(employeeDto); }

            var message = string.Empty;

            try
            {
                var result = await _employeeService.CreateEmployeeAsync(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee Isn't Created :(";
                    ModelState.AddModelError(string.Empty, "Employee Isn't Created ;(");
                    return View(employeeDto);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employeeDto);
                }
                else
                {
                    message = "Employee Isn't Created :(";
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

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            else return View(employee);

        }

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();//400 Request

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);

            if (employee is null) return NotFound();
            else
                return View(new EmployeeEditViewModel()
                {

                    Name = employee.Name,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    Age = employee.Age,
                    Email = employee.Email,
                    EmployeeType = employee.EmployeeType,
                    Gender = employee.Gender,
                    HiringDate = employee.HiringDate,
                    IsActive = employee.IsActive,
                    PhoneNumber = employee.PhoneNumber,

                }
                );
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeEditViewModel employeeEditViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeEditViewModel);
            }

            var message = string.Empty;

            try
            {
                var employeeToUpdate = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = employeeEditViewModel.Name,
                    Address = employeeEditViewModel.Address,
                    Salary = employeeEditViewModel.Salary,
                    Age = employeeEditViewModel.Age,
                    Email = employeeEditViewModel.Email,
                    EmployeeType = employeeEditViewModel.EmployeeType,
                    Gender = employeeEditViewModel.Gender,
                    HiringDate = employeeEditViewModel.HiringDate,
                    IsActive = employeeEditViewModel.IsActive,
                    PhoneNumber = employeeEditViewModel.PhoneNumber,


                };

                var result = await _employeeService.UpdateEmployeeAsync(employeeToUpdate) > 0;
                if (result) return RedirectToAction(nameof(Index));

                message = "An Error Occurred In Updating The Employee :(";
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);
                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred In Updating The Department :(";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(employeeEditViewModel);



        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null)
                return BadRequest();

            var employee = await _employeeService.GetEmployeeByIdAsync(id.Value);
            if (employee is null)
                return NotFound();

            return View(employee);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;
            try
            {

                var Deleted = await _employeeService.DeleteEmployeeAsync(id);
                if (Deleted)
                    return RedirectToAction(nameof(Index));
                message = "An Error Occurred While Deleting :(";

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An Error Occurred In deleting The Employee :(";

            }
            return RedirectToAction(nameof(Index));


        }
        #endregion

    }
}

using CompanyMVVM.Models;
using CompanyMVVM.ViewModel.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CompanyMVVM.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CompanyContext _context;

        public EmployeeController(CompanyContext context)
        {
            _context = context;
        }
        #region [List]
        /// <summary>
        /// Employee List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var EmployeeDepartment = _context.Employees
                    .Include(e => e.Department)
                    .Select(e => new EmployeeListViewModel
                    {
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.EmployeeName,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = e.Department.DepartmentName,
                        IdentificationNumber = e.IdentificationNumber,
                        IsActive = e.IsActive,
                        IsManager = e.IsManager
                    });
                return View("List", EmployeeDepartment.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("List");
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// View Employee Data
        /// </summary>
        /// <returns></returns>
        public IActionResult View(int id)
        {
            try
            {
                var EmployeeDepartment = _context.Employees
                                         .Include(e => e.Department)
                                         .Select(e => new EmployeeViewViewModel
                                         {
                                             EmployeeId = e.EmployeeId,
                                             EmployeeName = e.EmployeeName,
                                             DepartmentId = e.DepartmentId,
                                             DepartmentName = e.Department.DepartmentName,
                                             IdentificationNumber = e.IdentificationNumber,
                                             IsActive = e.IsActive,
                                             IsManager = e.IsManager,
                                             MobileNumber = e.MobileNumber,
                                             HomeNumber = e.HomeNumber,
                                             WorkNumber = e.WorkNumber,
                                             Code = e.Code,
                                             Salary = e.Salary
                                         })
                                         .Where(e => e.EmployeeId == id)
                                         .FirstOrDefault();
                return View(EmployeeDepartment);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult View()
        {
            try
            {
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region [Add]
        /// <summary>
        /// Add Employee
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add(string ErrorMessage)
        {
            EmployeeAddViewModel employeeAddViewModel = new EmployeeAddViewModel();
            try
            {
                if (ErrorMessage != null)
                {
                    ViewBag.ErrorMessage = ErrorMessage;
                }
                /**************************************************/
                //To set in the dropdown list (DepartmentName)//
                /**************************************************/
                List<string> DepartmentNameList = _context.Departments.Select(d => d.DepartmentName).ToList();
                employeeAddViewModel.DepartmentIDListItem = new List<SelectListItem>();
                foreach (var item in DepartmentNameList)
                {
                    SelectListItem newLst = new SelectListItem()
                    {
                        Text = item.ToString(),
                        Value = item.ToString()
                    };
                    employeeAddViewModel.DepartmentIDListItem.Add(newLst);
                }
                /**************************************************/
                //To set the radio button yes and no//
                /**************************************************/
                employeeAddViewModel.RadioButtonListItem = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Yes",Value="true"},
                    new SelectListItem() {Text="No",Value="false"}
                };
                /**************************************************/
                return View("Add", employeeAddViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.InnerException.Message;
                return View("Add");
            }
            finally
            {
                employeeAddViewModel = null;
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] EmployeeAddViewModel employeeVM)
        {
            Employee employee = new Employee();
            try
            {
                int departmentID = _context.Departments
                    .Where(d => d.DepartmentName == employeeVM.DepartmentName)
                    .Select(d => d.DepartmentId)
                    .FirstOrDefault();
                employee = new Employee()
                {
                    EmployeeName = employeeVM.EmployeeName,
                    DepartmentId = departmentID,
                    Code = employeeVM.Code,
                    HomeNumber = employeeVM.HomeNumber,
                    MobileNumber = employeeVM.MobileNumber,
                    WorkNumber = employeeVM.WorkNumber,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    IsManager = employeeVM.IsManager,
                    IdentificationNumber = employeeVM.IdentificationNumber,
                };
                if (employee.Salary <= 0)
                {
                    throw new Exception("Salary must be greater than 0");
                }
                if (employee.Code <= 0)
                {
                    throw new Exception("Code must be greater than 0");
                }
                _context.Add(employee);
                _context.SaveChanges();
                return Json(new { result = "Succeded" });
            }
            catch (Exception ex)
            {
                string ErrorMessage;
                if (ex.InnerException != null)
                    ErrorMessage = ex.InnerException.Message;
                else
                    ErrorMessage = ex.Message;

                return Json(new { result = ErrorMessage });
            }
        }
        #endregion

        #region [Edit]
        /// <summary>
        /// Edit Employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel();
            try
            {
                /**************************************************/
                //To set in the dropdown list (DepartmentName)//
                /**************************************************/
                List<string> DepartmentNameList = _context.Departments.Select(d => d.DepartmentName).ToList();
                employeeEditViewModel.DepartmentIDListItem = new List<SelectListItem>();
                foreach (var item in DepartmentNameList)
                {
                    SelectListItem newLst = new SelectListItem()
                    {
                        Text = item.ToString(),
                        Value = item.ToString()
                    };
                    employeeEditViewModel.DepartmentIDListItem.Add(newLst);
                }
                /**************************************************/
                //To set the radio button yes and no//
                /**************************************************/
                employeeEditViewModel.RadioButtonListItem = new List<SelectListItem>()
                {
                    new SelectListItem() {Text="Yes",Value="true"},
                    new SelectListItem() {Text="No",Value="false"}
                };
                /**************************************************/
                var employee = _context.Employees
                    .Where(e => e.EmployeeId == id)
                    .Select(e => new EmployeeEditViewModel
                    {
                        EmployeeId = e.EmployeeId,
                        EmployeeName = e.EmployeeName,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = e.Department.DepartmentName,
                        IdentificationNumber = e.IdentificationNumber,
                        IsActive = e.IsActive,
                        IsManager = e.IsManager,
                        MobileNumber = e.MobileNumber,
                        HomeNumber = e.HomeNumber,
                        WorkNumber = e.WorkNumber,
                        Code = e.Code,
                        Salary = e.Salary,
                        DepartmentIDListItem = employeeEditViewModel.DepartmentIDListItem,
                        RadioButtonListItem = employeeEditViewModel.RadioButtonListItem
                    })
                    .FirstOrDefault();
                return View(employee);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit([FromBody] EmployeeEditViewModel employeeVM)
        {
            try
            {
                Employee employee = new Employee();

                int departmentID = _context.Departments
                        .Where(d => d.DepartmentName == employeeVM.DepartmentName)
                        .Select(d => d.DepartmentId)
                        .FirstOrDefault();
                employee = new Employee()
                {
                    EmployeeId = employeeVM.EmployeeId,
                    EmployeeName = employeeVM.EmployeeName,
                    DepartmentId = departmentID,
                    Code = employeeVM.Code,
                    HomeNumber = employeeVM.HomeNumber,
                    MobileNumber = employeeVM.MobileNumber,
                    WorkNumber = employeeVM.WorkNumber,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    IsManager = employeeVM.IsManager,
                    IdentificationNumber = employeeVM.IdentificationNumber,
                };

                if (employee.Salary <= 0)
                {
                    throw new Exception("Salary must be greater than 0");
                }
                if (employee.Code <= 0)
                {
                    throw new Exception("Code must be greater than 0");
                }

                _context.Update(employee);
                _context.SaveChanges();
                return Json(new { result = "Succeded" });
            }
            catch (Exception ex)
            {
                string ErrorMessage;
                if (ex.InnerException != null)
                    ErrorMessage = ex.InnerException.Message;
                else
                    ErrorMessage = ex.Message;

                return Json(new { result = ErrorMessage });
            }
        }
        #endregion

        #region [Delete]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var Employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == id);
                _context.Employees.Remove(Employee);
                _context.SaveChanges();
                return Json(new { result = "Succeded" });
            }
            catch (Exception ex)
            {
                string ErrorMessage;
                if (ex.InnerException != null)
                    ErrorMessage = ex.InnerException.Message;
                else
                    ErrorMessage = ex.Message;
                return Json(new { result = ErrorMessage });
            }
        }
        #endregion
    }
}

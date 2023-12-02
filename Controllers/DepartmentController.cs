using Microsoft.AspNetCore.Mvc;
using CompanyMVVM.Models;
using CompanyMVVM.ViewModel.Department;

namespace CompanyMVVM.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly CompanyContext _context;
        public DepartmentController(CompanyContext context)
        {
            _context = context;
        }

        #region [List]
        /// <summary>
        /// Get list of Departments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List(string ErrorMessage)
        {
            try
            {
                ViewBag.ErrorMessage = ErrorMessage;

                var DepartmentData = _context.Departments.Select(d => new DepartmentListViewModel
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    DepartmentNumber = d.DepartmentNumber,
                    Budget = d.Budget
                });
                
                return View(DepartmentData.ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region [View]
        /// <summary>
        /// View Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult View(int id)
        {
            try
            {
                var data = _context.Departments
                    .Where(d => d.DepartmentId == id)
                    .Select(d=>new DepartmentViewViewModel
                    {
                        DepartmentName=d.DepartmentName,
                        DepartmentNumber=d.DepartmentNumber,
                        Budget=d.Budget
                    })
                    .FirstOrDefault();
                return View(data);
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
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region [Add]
        /// <summary>
        /// Add Department
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        public IActionResult Add(DepartmentAddViewModel departmentVM)
        {
            Department department;
            try
            {
                department = new Department()
                {
                    DepartmentId = departmentVM.DepartmentId,
                    DepartmentName = departmentVM.DepartmentName,
                    DepartmentNumber = departmentVM.DepartmentNumber,
                    Budget = departmentVM.Budget
                };
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                department=null;
            }
        }

        #endregion

        #region [Edit]
        /// <summary>
        /// Edit Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var data = _context.Departments
                    .Where(d => d.DepartmentId == id)
                    .Select(d=>new DepartmentEditViewModel()
                    {
                        DepartmentId=d.DepartmentId,
                        DepartmentName=d.DepartmentName,
                        DepartmentNumber=d.DepartmentNumber,
                        Budget=d.Budget
                    }).FirstOrDefault();

                return View(data);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit(DepartmentEditViewModel DepartmentVM)
        {
            Department department=null;
            try
            {
                department = new Department()
                {
                    DepartmentId = DepartmentVM.DepartmentId,
                    DepartmentName=DepartmentVM.DepartmentName,
                    DepartmentNumber=DepartmentVM.DepartmentNumber,
                    Budget = DepartmentVM.Budget
                };
                _context.Departments.Update(department);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                throw;
            }
            finally { department=null; }
        }

        #endregion

        #region [Delete]

        [HttpGet]
        [ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var department = _context.Departments.FirstOrDefault(d => d.DepartmentId == id);
                _context.Departments.Remove(department);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage=ex.InnerException.Message;
                return RedirectToAction("List", new { ErrorMessage = ex.InnerException.Message.ToString() });
            }
        }

        #endregion

    }
}

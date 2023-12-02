using CompanyMVVM.Models;
using CompanyMVVM.ViewModel.Child;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVVM.Controllers
{
    public class ChildController : Controller
    {
        private readonly CompanyContext _context;

        public ChildController(CompanyContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region [List]
        /// <summary>
        /// List of child
        /// </summary>
        /// <returns></returns>
        public IActionResult List(int EmployeeId)
        {
            try
            {
                var ChildVM = _context.Children
                    .Where(e=>e.EmployeeId == EmployeeId)
                    .Select(c => new ChildListViewModel
                    {
                        ChildId = c.ChildId,
                        EmployeeId=EmployeeId,
                        ChildName = c.ChildName,
                        ChildAge = c.ChildAge
                    }).ToList();

                if (ChildVM.Count == 0)
                {
                    var child = new ChildListViewModel
                    {
                        EmployeeId = EmployeeId
                    };

                    ChildVM.Add(child);
                }

                return View(ChildVM);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region [Add]
        /// <summary>
        /// Add child
        /// </summary>
        /// <returns></returns>
        public IActionResult Add(int EmployeeID)
        {
            ChildAddViewModel ChildVM = new ChildAddViewModel();
            try
            {
                ChildVM.EmployeeId = EmployeeID;
                return View(ChildVM);
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                ChildVM = null;
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] ChildAddViewModel ChildVM)
        {
            Child child = new Child();
            try
            {
                child = new Child()
                {
                    EmployeeId = ChildVM.EmployeeId,
                    ChildAge = ChildVM.ChildAge,
                    ChildName = ChildVM.ChildName,
                };

                _context.Add(child);
                _context.SaveChanges();

                return Json(new { result = "success" });
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
            finally
            {
                child = null;
            }
        }

        #endregion

        #region [Edit]
        /// <summary>
        /// Edit Child
        /// </summary>
        /// <param name="ChildID"></param>
        /// <returns></returns>
        public IActionResult Edit(int ChildID)
        {
            try
            {
                var ChildVM = _context.Children
                                .Where(c => c.ChildId == ChildID)
                                .Select(c => new ChildEditViewModel
                                {
                                    ChildId = c.ChildId,
                                    EmployeeId=c.EmployeeId,
                                    ChildName = c.ChildName,
                                    ChildAge = c.ChildAge
                                }).FirstOrDefault();
                return View(ChildVM);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public IActionResult Edit([FromBody] ChildEditViewModel ChildVM)
        {
            Child child = new Child();
            try
            {
                child=new Child() 
                {
                    ChildId= ChildVM.ChildId,
                    EmployeeId = ChildVM.EmployeeId,
                    ChildAge = ChildVM.ChildAge,
                    ChildName= ChildVM.ChildName,
                };

                _context.Update(child);
                _context.SaveChanges();

                return Json(new { result = "success" });
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
            finally
            {
                child = null;
            }
        }
        #endregion

        #region [Delete]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var Child = _context.Children.FirstOrDefault(e => e.ChildId == id);
                _context.Children.Remove(Child);
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


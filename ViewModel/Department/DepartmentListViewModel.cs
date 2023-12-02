

using CompanyMVVM.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyMVVM.ViewModel.Department
{
    public class DepartmentListViewModel:DbContext
    {
        public int DepartmentId { get; set; }

        public int DepartmentNumber { get; set; }

        public string DepartmentName { get; set; } = null!;

        public double? Budget { get; set; }
    }
}

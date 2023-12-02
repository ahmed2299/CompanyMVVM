namespace CompanyMVVM.ViewModel.Employee
{
    public class EmployeeListViewModel
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = null!;

        public string IdentificationNumber { get; set; } = null!;

        public bool IsManager { get; set; }

        public bool IsActive { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; } = null!;
    }
}

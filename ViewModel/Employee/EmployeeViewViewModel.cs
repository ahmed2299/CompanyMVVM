namespace CompanyMVVM.ViewModel.Employee
{
    public class EmployeeViewViewModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public string IdentificationNumber { get; set; } = null!;
        public int Code { get; set; }
        public double Salary { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string? HomeNumber { get; set; }
        public string? WorkNumber { get; set; }
        public bool IsManager { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = null!;
    }
}

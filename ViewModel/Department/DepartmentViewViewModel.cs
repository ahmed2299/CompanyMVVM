namespace CompanyMVVM.ViewModel.Department
{
    public class DepartmentViewViewModel
    {
        public int DepartmentId { get; set; }
        public int DepartmentNumber { get; set; }
        public string DepartmentName { get; set; } = null!;
        public double? Budget { get; set; }
    }
}

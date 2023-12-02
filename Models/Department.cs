using System;
using System.Collections.Generic;

namespace CompanyMVVM.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public int DepartmentNumber { get; set; }

    public string DepartmentName { get; set; } = null!;

    public double? Budget { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

using System;
using System.Collections.Generic;

namespace CompanyMVVM.Models;

public partial class Employee
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

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual Department Department { get; set; } = null!;
}

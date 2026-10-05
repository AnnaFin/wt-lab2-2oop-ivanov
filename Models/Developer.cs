using System;

namespace WtLab2.Models;

public class Developer : BaseEntity
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Level { get; private set; }
    public decimal Salary { get; private set; }

    public Developer(string fullName, string email, string level, decimal salary)
    {
        SetEntityType("Developer");
        FullName = fullName;
        Email = email;
        Level = level;
        Salary = salary;
    }

    public void UpdateSalary(decimal newSalary)
    {
        if (newSalary < 0)
            throw new ArgumentException("Зарплата не может быть отрицательной.");

        Salary = newSalary;
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"[Developer] Id={Id}, FullName={FullName}, Email={Email}, " +
            $"Level={Level}, Salary={Salary:C}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm}");
    }

    public override string GetStorageInfo()
        => base.GetStorageInfo() + $", FullName={FullName}";
}
using System;

namespace WtLab2.Models;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Customer { get; set; }

    // Инкапсуляция: изменить бюджет можно только через UpdateBudget
    public decimal Budget { get; private set; }

    // Инкапсуляция: статус меняется через ChangeStatus
    public string Status { get; private set; }

    public DateTime Deadline { get; set; }

    public Project(string name, string customer, decimal budget, DateTime deadline, string status = "New")
    {
        SetEntityType("Project");
        Name = name;
        Customer = customer;
        Budget = budget;
        Deadline = deadline;
        Status = status;
    }

    public void UpdateBudget(decimal newBudget)
    {
        if (newBudget < 0)
            throw new ArgumentException("Бюджет не может быть отрицательным.");

        Budget = newBudget;
    }

    public void ChangeStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus))
            throw new ArgumentException("Статус не может быть пустым.");

        Status = newStatus;
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"[Project] Id={Id}, Name={Name}, Customer={Customer}, Budget={Budget:C}, " +
            $"Deadline={Deadline:yyyy-MM-dd}, Status={Status}, CreatedAt={CreatedAt:yyyy-MM-dd HH:mm}");
    }

    public override string GetStorageInfo()
        => base.GetStorageInfo() + $", Name={Name}";
}
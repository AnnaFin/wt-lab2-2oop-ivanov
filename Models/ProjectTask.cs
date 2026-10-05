using System;

namespace WtLab2.Models;

public class ProjectTask : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public Guid ProjectId { get; private set; }
    public Guid? DeveloperId { get; private set; }
    public string Priority { get; private set; }
    public string Status { get; private set; }
    public int EstimatedHours { get; private set; }

    public ProjectTask(
        string title,
        string description,
        Guid projectId,
        string priority,
        int estimatedHours,
        string status = "New")
    {
        SetEntityType("ProjectTask");
        Title = title;
        Description = description;
        ProjectId = projectId;
        Priority = priority;
        EstimatedHours = estimatedHours;
        Status = status;
    }

    public void AssignDeveloper(Guid developerId)
    {
        DeveloperId = developerId;
    }

    public void ChangeStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus))
            throw new ArgumentException("Статус не может быть пустым.");

        Status = newStatus;
    }

    public void UpdateEstimatedHours(int hours)
    {
        if (hours <= 0)
            throw new ArgumentException("Оценка должна быть больше 0.");

        EstimatedHours = hours;
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"[ProjectTask] Id={Id}, Title={Title}, ProjectId={ProjectId}, " +
            $"DeveloperId={DeveloperId?.ToString() ?? "не назначен"}, " +
            $"Priority={Priority}, Status={Status}, Hours={EstimatedHours}");
    }

    public override string GetStorageInfo()
        => base.GetStorageInfo() + $", Title={Title}";
}
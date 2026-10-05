using System;
using WtLab2.Interfaces;

namespace WtLab2.Models;

public abstract class BaseEntity : IStorable
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    // protected-член: доступен только наследникам
    protected string EntityType { get; set; } = "Entity";

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    protected BaseEntity(Guid id, DateTime createdAt)
    {
        Id = id;
        CreatedAt = createdAt;
    }

    public abstract void PrintInfo();

    public virtual string GetStorageInfo()
        => $"{EntityType} #{Id} created {CreatedAt:yyyy-MM-dd HH:mm}";

    // protected-метод: используется наследниками
    protected void SetEntityType(string type)
    {
        EntityType = type;
    }
}
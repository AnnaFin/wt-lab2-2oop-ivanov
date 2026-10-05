using System;

namespace WtLab2.Interfaces;

public interface IStorable
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    string GetStorageInfo();
}
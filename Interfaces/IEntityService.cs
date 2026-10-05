using System;
using System.Collections.Generic;
using WtLab2.Models;

namespace WtLab2.Interfaces;

public interface IEntityService<T> where T : BaseEntity
{
    void Add(T item);
    void Add(IEnumerable<T> items); // перегрузка метода
    T? GetById(Guid id);
    IReadOnlyCollection<T> GetAll();
    bool Update(T item);
    bool Delete(Guid id);
    int Count { get; }
}
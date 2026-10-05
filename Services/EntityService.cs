using System;
using System.Collections.Generic;
using WtLab2.Interfaces;
using WtLab2.Models;

namespace WtLab2.Services;

public class EntityService<T> : IEntityService<T> where T : BaseEntity
{
    private readonly List<T> _items = new();
    private readonly Dictionary<Guid, T> _byId = new();

    public int Count => _items.Count;

    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (_byId.ContainsKey(item.Id))
            throw new InvalidOperationException("Элемент с таким Id уже существует.");

        _items.Add(item);
        _byId.Add(item.Id, item);
    }

    // Перегрузка метода Add
    public void Add(IEnumerable<T> items)
    {
        if (items == null)
            throw new ArgumentNullException(nameof(items));

        foreach (var item in items)
        {
            Add(item);
        }
    }

    public T? GetById(Guid id)
    {
        return _byId.TryGetValue(id, out var item) ? item : null;
    }

    public IReadOnlyCollection<T> GetAll()
    {
        return _items.AsReadOnly();
    }

    public bool Update(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));

        if (!_byId.ContainsKey(item.Id))
            return false;

        var index = _items.FindIndex(x => x.Id == item.Id);
        if (index < 0)
            return false;

        _items[index] = item;
        _byId[item.Id] = item;
        return true;
    }

    public bool Delete(Guid id)
    {
        if (!_byId.TryGetValue(id, out var item))
            return false;

        _byId.Remove(id);
        _items.Remove(item);
        return true;
    }
}
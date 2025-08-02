namespace Egui.Util;

/// <summary>
/// Stores values identified by an <see cref="Id"/> AND the <see cref="Type"/> of the value.<br/>
/// In other words, it maps (Id, TypeId) to any value you want.
/// You can store state using the key <see cref="Id.Null"/>. The state will then only be identified by its type.
/// </summary>
public ref struct IdTypeMap
{
    /// <summary>
    /// Holds C#-side objects.
    /// </summary>
    private readonly Dictionary<(Id, Type), object> _inner;

    public bool IsEmpty => _inner.Count == 0;

    public int Length => _inner.Count;

    /// <summary>
    /// Creates a new map wrapper.
    /// </summary>
    /// <param name="inner">The inner map to modify.</param>
    internal IdTypeMap(Dictionary<(Id, Type), object> inner)
    {
        _inner = inner;
    }

    public void Clear()
    {
        _inner.Clear();
    }

    /// <summary>
    /// Count the number of values are stored with the given type.
    /// </summary>
    public int Count<T>()
    {
        return _inner.Where(x => typeof(T) == x.Key.Item2).Count();
    }

    /// <summary>
    /// Reads a value without trying to deserialize a persisted value.
    /// </summary>
    public T? GetTemp<T>(Id id) where T : struct
    {
        if (_inner.TryGetValue((id, typeof(T)), out var value))
        {
            return (T?)value;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Insert a value that will not be persisted.
    /// </summary>
    public void InsertTemp<T>(Id id, T value) where T : struct
    {
        _inner[(id, typeof(T))] = value;
    }

    /// <summary>
    /// Remove the state of this type and id.
    /// </summary>
    public void Remove<T>(Id id) where T : struct
    {
        _inner.Remove((id, typeof(T)));
    }

    /// <summary>
    /// Note all state of the given type.
    /// </summary>
    public void RemoveByType<T>() where T : struct
    {
        foreach (var (id, ty) in _inner.Keys)
        {
            if (typeof(T) == ty)
            {
                _inner.Remove((id, ty));
            }
        }
    }

    /// <summary>
    /// Remove and fetch the state of this type and id.
    /// </summary>
    public T? RemoveTemp<T>(Id id) where T : struct
    {
        if (_inner.Remove((id, typeof(T)), out var value))
        {
            return (T)value;
        }
        else
        {
            return null;
        }
    }
}
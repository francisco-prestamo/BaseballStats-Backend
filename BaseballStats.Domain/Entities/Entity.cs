using BaseballStats.Domain.Interfaces.IEntity;

namespace BaseballStats.Domain.Entities;

public abstract class Entity<T> : Entity where T : struct
{
    public required T Id { get; set; }
}

public abstract class Entity : IEntity
{
}
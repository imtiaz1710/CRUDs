using System;

namespace ECommerceSystem.Data
{
    public interface IEntity<T>
    {
        T Id { get; set; } 
    }
}

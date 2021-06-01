using System;
using System.Collections.Generic;

namespace Cinema.Persistence.Models
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

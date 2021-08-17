using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }

        bool IsObjectNull();

        bool IsEmptyObject();
    }
}

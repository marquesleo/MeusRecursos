using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Extension
{
    public static class IEntityExtensions
    {
        public static bool IsObjectNull(this IEntity entity)
        {
            return entity == null;
        }

        public static bool IsEmptyObject(this IEntity entity)
        {
            return entity?.Id == null || entity?.Id == Guid.Empty;
        }
    }
}

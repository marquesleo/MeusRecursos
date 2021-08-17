using Entities.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    public class Base : IEntity
    {
        [Column("id")]
        public Guid Id { get ; set; }

        public bool IsEmptyObject()
        {
            return IEntityExtensions.IsEmptyObject(this);
        }
        public bool IsObjectNull()
        {
            return IEntityExtensions.IsObjectNull(this);
        }
    }
}

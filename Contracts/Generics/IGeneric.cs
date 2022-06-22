using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Generics
{
   public  interface IGeneric<T> where T : class
    {
        Task Add(T objeto);
        Task Update(T objeto);
        Task Delete(T objeto);
        Task<T> GetEntityById(Guid id);
        Task<List<T>> List();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);

    }
}

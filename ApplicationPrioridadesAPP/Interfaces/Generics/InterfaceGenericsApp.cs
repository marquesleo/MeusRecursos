using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.Interfaces.Generics
{
    public interface InterfaceGenericsApp<T> where T : class
    {
        
        Task Delete(T objeto);
        Task<T> GetEntityById(Guid id);
        Task<List<T>> List();

        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);

    }
}

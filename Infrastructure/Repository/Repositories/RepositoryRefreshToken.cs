using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Infrastructure.Configuration;
using System.Threading.Tasks;
using System.Linq;
using Domain.Prioridades.Interfaces;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryRefreshToken : Generics.RepositoryGeneric<RefreshToken>, IRefreshToken
    {
        public RepositoryRefreshToken(MyDB myDB):base(myDB){ }
              

       
    }
}

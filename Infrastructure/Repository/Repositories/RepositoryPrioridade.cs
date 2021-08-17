using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Infrastructure.Configuration;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryPrioridade : Generics.RepositoryGeneric<Prioridade>, IPrioridade
    {
        public RepositoryPrioridade(MyDB myDB):base(myDB){ }
    }
}

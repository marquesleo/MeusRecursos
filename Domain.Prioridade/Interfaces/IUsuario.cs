using Contracts.Generics;
using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace Domain.Prioridades.Interface
{
    public interface IUsuario: IGeneric<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
    }
}

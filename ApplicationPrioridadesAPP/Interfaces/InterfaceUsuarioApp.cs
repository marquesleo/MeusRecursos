using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfaceUsuarioApp :Generics.InterfaceGenericsApp<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
        Task AddUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
    }
}

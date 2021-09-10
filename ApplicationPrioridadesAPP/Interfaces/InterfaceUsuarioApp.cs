using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfaceUsuarioApp :Generics.InterfaceGenericsApp<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
        Task AddUsuario(LoginViewModel loginViewModel);
        Task UpdateUsuario(Usuario usuario);
    }
}

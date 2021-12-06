using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.Interfaces
{
    public interface InterfaceUsuarioApp :Generics.InterfaceGenericsApp<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
        bool IsUsuarioExiste(string login);
        Task Incluir(LoginViewModel loginViewModel);
        Task Alterar(Usuario usuario);
    }
}

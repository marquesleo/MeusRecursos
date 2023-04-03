using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfaceUsuarioApp :Generics.InterfaceGenericsApp<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
        bool IsUsuarioExiste(string login);
        Task AddUsuario(LoginViewModel loginViewModel);
        Task UpdateUsuario(LoginViewModel loginViewModel);
        AuthenticateResponse Authenticate(Usuario usuario, string ipAddress);
        Task <AuthenticateResponse> RefreshToken(string token, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
    }
}

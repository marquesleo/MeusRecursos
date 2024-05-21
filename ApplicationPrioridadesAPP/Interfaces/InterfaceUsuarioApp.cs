using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfaceUsuarioApp :Generics.InterfaceGenericsApp<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
        bool IsUsuarioExiste(string login);
        bool IsUsuarioComEmailExistente(string email);
        Task AddUsuario(LoginViewModel loginViewModel);
        Task UpdateUsuario(LoginViewModel loginViewModel);
        Task AddUsuario(Usuario usuario);
        Task UpdateUsuario(Usuario usuario);
        AuthenticateResponse Authenticate(Usuario usuario, string ipAddress);
        Task <AuthenticateResponse> RefreshToken(string token, string refreshToken, string ipAddress);
        Task<bool> RevokeToken(string token, string ipAddress);
    }
}

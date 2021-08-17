using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace Domain.Prioridades.InterfaceService
{
    public interface IServiceUsuario { 
        Task AddUsuario(Usuario usuario);
        Task UsuarioPrioridade(Usuario usuario);
    }
}

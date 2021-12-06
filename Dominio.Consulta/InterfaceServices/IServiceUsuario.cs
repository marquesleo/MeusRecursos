using Domain.Consulta.Entities;
using System.Threading.Tasks;

namespace Domain.Consulta.InterfaceService
{
    public interface IServiceUsuario { 
        Task AddUsuario(Usuario usuario);
        Task UsuarioPrioridade(Usuario usuario);
    }
}

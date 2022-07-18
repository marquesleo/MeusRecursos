using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace Domain.Prioridades.InterfaceService
{
    public interface IServiceSenha{
        Task AddSenha(Senha Senha);
        Task UpdateSenha(Senha senha);
      
    }
}

using Domain.Prioridades.Entities;
using System.Threading.Tasks;

namespace Domain.Prioridades.InterfaceService
{
    public interface IServicePrioridade
    {
        Task AddPrioridade(Prioridade prioridade);
        Task UpdatePrioridade(Prioridade prioridade);
    }
}

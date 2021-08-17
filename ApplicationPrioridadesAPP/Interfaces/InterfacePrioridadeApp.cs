using Domain.Prioridades.Entities;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfacePrioridadeApp : Generics.InterfaceGenericsApp<Prioridade>
    {
        Task AddPrioridade(Prioridade prioridade);
        Task UpdatePrioridade(Prioridade prioridade);
    }
}

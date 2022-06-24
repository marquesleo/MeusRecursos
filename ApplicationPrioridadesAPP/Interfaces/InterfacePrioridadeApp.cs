using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfacePrioridadeApp : Generics.InterfaceGenericsApp<Prioridade>
    {
        Task AddPrioridade(Prioridade prioridade);
        Task UpdatePrioridade(Prioridade prioridade);

        Task Up(Prioridade prioridade);

        Task Down(Prioridade prioridade);

        Task<List<PrioridadeViewModel>>  ObterPrioridade(string id_usuario, bool? feito=false);


    }
}

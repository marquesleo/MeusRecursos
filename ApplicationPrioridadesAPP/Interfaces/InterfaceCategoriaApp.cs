using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.Interfaces
{

 
    public interface InterfaceCategoriaApp : Generics.InterfaceGenericsApp<Categoria>
    {
        Task AddCategoria(Categoria categoria);
        Task UpdateCategoria(Categoria categoria);
        Task<List<Categoria>> ObterCategoria(string id_usuario);

    }
}

using ApplicationPrioridadesAPP.Interfaces.Generics;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfaceSenhaApp : InterfaceGenericsApp<Senha>
    {
        Task AddSenha(Senha senha);
        Task UpdateSenha(Senha senha);
        Task ExcluirSenha(Senha senha);
        Task<List<SenhaViewModel>> ObterRegistros(string id_usuario);

    }
}

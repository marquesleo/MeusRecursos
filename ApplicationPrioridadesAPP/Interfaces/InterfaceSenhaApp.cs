using ApplicationPrioridadesAPP.Interfaces.Generics;
using Domain.Prioridades.Entities;
using Domain.Prioridades.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.Interfaces
{
    public interface InterfaceSenhaApp : InterfaceGenericsApp<Senha>
    {
        Task AddSenha(SenhaViewModel senha);
        Task UpdateSenha(SenhaViewModel senha);
        Task<List<SenhaViewModel>> ObterRegistros(string id_usuario);

        Task<SenhaViewModel> GetSenhaById(string id);

    }
}

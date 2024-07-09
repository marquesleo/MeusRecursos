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
        Task<bool> CriptografarTudo();
        Task<List<Senha>> ObterRegistros(string id_usuario);

        Task<List<SenhaViewModel>> ObterRegistros(string id_usuario,
                                                  string descricao);

        Task<List<Senha>> ObterRegistrosPorFiltros(string id_usuario,
                                          string descricao);

        Task<Senha> GetSenhaById(string id);

    }
}

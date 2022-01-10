using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using System;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.Interfaces
{
    public interface InterfaceAcessoApp: Generics.InterfaceGenericsApp<Acesso>
    {
        Task Incluir(AcessoViewModel acessoViewModel);
        Task Alterar(AcessoViewModel acessoViewModel);
        Task<Acesso> ObterAcessoPorUsuarioEEmpresa(Guid idUsuario, Guid idEmpresa);
        Task<Acesso> ObterAcessoPorUsuario(Guid idUsuario);
    }
}

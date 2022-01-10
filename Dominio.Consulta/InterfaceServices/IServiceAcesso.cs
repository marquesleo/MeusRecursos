using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.InterfaceServices
{
    public interface IServiceAcesso
    {
        Task IncluirAcesso(Acesso acesso);
        Task AlterarAcesso(Acesso acesso);
        Task<Acesso> ObterAcessoPorId(Guid id);
        Task<Acesso> ObterAcessoPorUsuarioEEmpresa(Guid idUsuario, Guid idEmpresa);
    }
}

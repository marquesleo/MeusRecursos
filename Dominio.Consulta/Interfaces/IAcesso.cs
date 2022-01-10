using Contracts.Generics;
using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Interfaces
{
    public interface IAcesso : IGeneric<Acesso>
    {
        
        Task<Acesso> ObterAcessoPorId(Guid id);
        Task<Acesso> ObterAcessoPorUsuarioEEmpresa(Guid idUsuario,Guid idEmpresa);
    }
}

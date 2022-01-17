using Contracts.Generics;
using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Consulta.Interface
{
    public interface IUsuario: IGeneric<Usuario>
    {
        Task<Usuario> ObterUsuario(string login, string senha);
        Task<Usuario> ObterUsuario(string login);
        Task<List<Usuario>> ObterPorEmpresa(Guid empresa_id);
    }
}

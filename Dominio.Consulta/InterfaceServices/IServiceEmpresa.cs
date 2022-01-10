using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.InterfaceServices
{
    public interface IServiceEmpresa
    {
        Task AddEmpresa(Empresa empresa);
        Task AlterarEmpresa(Empresa empresa);
        Task<Empresa> ObterEmpresa(string id);
        Task<List<Empresa>> ObterEmpresas(string nomeCompleto);
    }
}

using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.InterfaceServices
{
    public interface IServicePsicologa
    {
        Task AddPsicologa(Psicologa psicologa);
        Task AlterarPsicologa(Psicologa psicologa);
        Task<Psicologa> ObterPsicologa(string id);
        Task<List<Psicologa>> ObterPsicologos(Guid Empresa_Id);
    }
}

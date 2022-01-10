using Contracts.Generics;
using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Interfaces
{
    public interface IPsicologa : IGeneric<Psicologa>
    {
        Task<List<Psicologa>> ObterPsicologos(Guid Empresa_Id);
    }
}

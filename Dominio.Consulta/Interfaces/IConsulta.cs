using Contracts.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Consulta.Entities;
namespace Domain.Consulta.Interfaces
{
    public interface IConsulta : IGeneric<Entities.Consulta>
    {

        Task<bool> IsJaExisteMarcado(Guid empresa_id, DateTime diaHora);
        Task<bool> IsJaExisteMarcado(Guid empresa_id, Guid consulta_id, DateTime diaHora);
    }
}

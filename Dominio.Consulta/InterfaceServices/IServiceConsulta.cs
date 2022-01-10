using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.InterfaceServices
{
    public interface IServiceConsulta
    {
        Task AddConsulta(Entities.Consulta consulta);
        Task AlterarConsulta(Entities.Consulta consulta);
        Task Delete(Entities.Consulta consulta);
        Task<List<Entities.Consulta>> FindByCondition(Expression<Func<Entities.Consulta, bool>> expression);
        Task<Entities.Consulta> GetEntityById(Guid id);
        Task<List<Entities.Consulta>> List();
        Task<bool> IsJaExisteMarcado(Guid empresa_id, DateTime diaHora);
        Task<bool> IsJaExisteMarcado(Guid empresa_id, Guid consulta_id, DateTime diaHora);
        Task<List<Entities.Consulta>> ConsultarAgenda(Guid empresa_id, DateTime diaInicial, DateTime diaFinal, Guid paciente_id, Guid psicologo_id);
        Task<List<Entities.Consulta>> ConsultarAgendaNaoRealizada(Guid empresa_id, string dia, string paciente, string psicologa);

    }
}

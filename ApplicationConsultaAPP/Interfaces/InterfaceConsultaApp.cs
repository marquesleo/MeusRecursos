using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.Interfaces
{
    public interface InterfaceConsultaApp : Generics.InterfaceGenericsApp<Domain.Consulta.Entities.Consulta>
    {
        Task AddConsulta(ConsultaViewModel consulta);
        Task AlterarConsulta(ConsultaViewModel consulta);
        Task<bool> IsJaExisteMarcado(Guid empresa_id, DateTime diaHora);
        Task<bool> IsJaExisteMarcado(Guid empresa_id, Guid consulta_id, DateTime diaHora);

        Task<List<Domain.Consulta.Entities.Consulta>> ConsultarAgenda(Guid empresa_id,
                                                                      DateTime diaInicial,
                                                                      DateTime diaFinal,
                                                                      Guid paciente_id,
                                                                      Guid psicologo_id);


        Task<List<Domain.Consulta.Entities.Consulta>> ConsultarAgendaNaoRealizada(Guid empresa_id,
                                                                                  string dia,
                                                                                  string paciente,
                                                                                  string psicologa);


    }
}

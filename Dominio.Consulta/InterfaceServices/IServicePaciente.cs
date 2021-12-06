using Domain.Consulta.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.InterfaceService
{
    public interface IServicePaciente
    {
        Task AddPaciente(Paciente paciente);

        Task AlterarPaciente(Paciente paciente);
        Task<Paciente> ObterPaciente(string id);
        Task<List<Paciente>> ObterPacientes(string nomeCompleto);
    }
}

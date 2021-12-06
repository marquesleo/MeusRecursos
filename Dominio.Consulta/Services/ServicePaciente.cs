using Domain.Consulta.Entities;
using Domain.Consulta.Interface;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceService;
using Domain.Consulta.Validations;
using Notification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Consulta.Services
{
    public class ServicePaciente : BaseService, IServicePaciente
    {
        private readonly IPaciente _IPaciente;

        public ServicePaciente(IPaciente IPaciente,
                                  INotificador notificador) : base(notificador)
        {
            this._IPaciente = IPaciente;
        }

        public async Task AddPaciente(Paciente paciente)
        {
            if (!ExecutarValidacao(new PacienteValidation(), paciente)) return;
            await _IPaciente.Add(paciente);
        }

        public async Task AlterarPaciente(Paciente paciente)
        {
            if (!ExecutarValidacao(new PacienteValidation(), paciente)) return;
            await _IPaciente.Update(paciente);
        }

        public async  Task<Paciente> ObterPaciente(string id)
        {
            return  await _IPaciente.ObterPaciente(id);
        }

        public async Task<List<Paciente>> ObterPacientes(string nomeCompleto)
        {
            return await _IPaciente.ObterPacientes(nomeCompleto);
        }
    }
}

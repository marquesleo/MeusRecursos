using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.OpenApp
{
    public class AppPaciente : InterfacePacienteApp
    {
        private readonly IPaciente _IPaciente;
        public AppPaciente(IPaciente IPaciente)
        {
            this._IPaciente = IPaciente;
        }

        public async Task Incluir(PacienteViewModel pacienteViewModel)
        {
            var paciente = new Paciente();
            paciente.Bairro = pacienteViewModel.Bairro;
            paciente.Celular = pacienteViewModel.Celular;
            paciente.Cep = pacienteViewModel.Cep;
            paciente.Cidade = pacienteViewModel.Cidade;
            paciente.DtNascimento = pacienteViewModel.DtNascimento;
            paciente.Email = pacienteViewModel.Email;
            paciente.Endereco = pacienteViewModel.Endereco;
            await _IPaciente.Add(paciente);
            pacienteViewModel.Id = paciente.Id;
        }

        public async Task Delete(Paciente objeto)
        {
            await _IPaciente.Delete(objeto);
        }

        public async Task<List<Paciente>> FindByCondition(Expression<Func<Paciente, bool>> expression)
        {
            return await _IPaciente.FindByCondition(expression);
        }

        public async Task<Paciente> GetEntityById(Guid id)
        {
            return await _IPaciente.GetEntityById(id);
        }

        public async Task<List<Paciente>> List()
        {
            return await _IPaciente.List();
        }

        public async Task<Paciente> ObterPaciente(string id)
        {
            return await _IPaciente.ObterPaciente(id);
        }

        public async Task<List<Paciente>> ObterPacientes(string nomeCompleto)
        {
            return await _IPaciente.ObterPacientes(nomeCompleto);
        }

        public async Task Alterar(Paciente paciente)
        {
            await _IPaciente.Update(paciente);
        }

        public bool IsPacienteExiste(string nome, string email, string telefone)
        {
            var pacientes = FindByCondition(p => p.Email == email || p.Telefone == telefone || p.Nome == nome).Result;
            if (pacientes == null)
                return true;
            else
                return false;
            


        }
    }
}

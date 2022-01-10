using ApplicationConsultaAPP.Interfaces;
using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceService;
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
        private readonly IServicePaciente _servicePaciente;
        private readonly IAcesso _IAcesso;
        public AppPaciente(IPaciente IPaciente,
                           IServicePaciente ServicePaciente,
                           IAcesso IAcesso)
        {
            _IPaciente = IPaciente;
            _IAcesso = IAcesso;
            _servicePaciente = ServicePaciente;
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
            paciente.Telefone = pacienteViewModel.Telefone;
            paciente.Nome = pacienteViewModel.Nome;
            paciente.Ativo = pacienteViewModel.Ativo;
           
            paciente.Acesso_Id = pacienteViewModel.Acesso_Id;
            try
            {
                await _servicePaciente.AddPaciente(paciente);
                pacienteViewModel.Id = paciente.Id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
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
            return await _servicePaciente.ObterPaciente(id.ToString());
        }

        public async Task<List<Paciente>> List()
        {
            return await _servicePaciente.ObterPacientes("");
        }

        public async Task<Paciente> ObterPaciente(string id)
        {
            return await _servicePaciente.ObterPaciente(id);
        }

        public async Task<List<Paciente>> ObterPacientes(string nomeCompleto)
        {
            return await _servicePaciente.ObterPacientes(nomeCompleto);
        }

        public async Task Alterar(Paciente paciente)
        {
            try
            {
                await _servicePaciente.AlterarPaciente(paciente);
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }

        public bool IsPacienteExiste(string nome, string email, string telefone)
        {
            var pacientes = FindByCondition(p => p.Email == email || p.Telefone == telefone || p.Nome == nome).Result;
            if (pacientes != null && pacientes.Any())
                return true;
            else
                return false;
            


        }

        public async Task<List<Paciente>> ObterPorNome(string nome)
        {
            return await _servicePaciente.ObterPacientes(nome);

        }

        public async Task<List<Paciente>> ObterPacientes(Guid Empresa_Id)
        {
            return await _servicePaciente.ObterPacientes(Empresa_Id);
        }
    }
}

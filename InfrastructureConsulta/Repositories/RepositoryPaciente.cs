
using System.Threading.Tasks;
using System.Linq;
using Domain.Consulta.Entities;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using Domain.Consulta.Interfaces;
using System.Collections.Generic;
using System;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryPaciente :RepositoryGeneric<Paciente>, IPaciente
    {
        public RepositoryPaciente(MyDB myDB):base(myDB){ }

        public async Task<Paciente> ObterPaciente(string id)
        {
            var pacientes = await GetEntityById(Guid.Parse(id));
           
            return pacientes;
        }

        public async Task<List<Paciente>> ObterPacientes(string nomeCompleto)
        {
            if (nomeCompleto == null)
                nomeCompleto = string.Empty;
            var pacientes = await FindByCondition(p => p.Nome.ToUpper().Contains(nomeCompleto.ToUpper()));
            return pacientes;
        }

        public async Task<List<Paciente>> ObterPacientes(Guid Empresa_Id)
        {
            return await FindByCondition(p => p.Acesso.Empresa_Id == Empresa_Id);
        }
    }
}

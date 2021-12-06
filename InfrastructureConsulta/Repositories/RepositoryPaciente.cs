
using System.Threading.Tasks;
using System.Linq;
using Domain.Consulta.Entities;
using Domain.Consulta.Interface;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using Domain.Consulta.Interfaces;
using System.Collections.Generic;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryPaciente :RepositoryGeneric<Paciente>, IPaciente
    {
        public RepositoryPaciente(MyDB myDB):base(myDB){ }

        public async Task<Paciente> ObterPaciente(string id)
        {
            var pacientes = await FindByCondition(p => p.Id.Equals(id));


            return pacientes.FirstOrDefault();
        }

        public async Task<List<Paciente>> ObterPacientes(string nomeCompleto)
        {
            var pacientes = await FindByCondition(p => p.Nome.Contains(nomeCompleto));
            return pacientes;
        }
    }
}

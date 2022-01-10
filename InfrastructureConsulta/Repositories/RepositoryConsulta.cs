using Domain.Consulta.Interfaces;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryConsulta : RepositoryGeneric<Domain.Consulta.Entities.Consulta>, IConsulta
    {
        public RepositoryConsulta(MyDB myDB) : base(myDB) { }

        public async Task<bool> IsJaExisteMarcado(Guid empresa_id, DateTime diaHora)
        {
            var retorno = await FindByCondition(p => p.Paciente.Acesso.Empresa_Id == empresa_id && p.Horario == diaHora && p.Status == "N");

            if (retorno.Count > 0)
                return true;

            return false;
        }

        public async Task<bool> IsJaExisteMarcado(Guid empresa_id, Guid consulta_id, DateTime diaHora)
        {
            var retorno = await FindByCondition(p => p.Paciente.Acesso.Empresa_Id == empresa_id &&
                                                     p.Horario == diaHora &&
                                                     p.Status == "N" && 
                                                     p.Id != consulta_id);

            if (retorno.Count > 0)
                return true;

            return false;
        }
    }
}

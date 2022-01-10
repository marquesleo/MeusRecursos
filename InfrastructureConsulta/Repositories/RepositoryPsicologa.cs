using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryPsicologa : RepositoryGeneric<Psicologa>, IPsicologa
    {
        public RepositoryPsicologa(MyDB myDB) : base(myDB) { }

        public async Task<List<Psicologa>> ObterPsicologos(Guid Empresa_Id)
        {
            return await FindByCondition(p => p.Acesso.Empresa_Id == Empresa_Id);
        }
    }
}

using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Infrastructure.Consulta.Configuration;
using Infrastructure.Consulta.Repository.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Consulta.Repository.Repositories
{
    public class RepositoryEmpresa : RepositoryGeneric<Empresa>, IEmpresa
    {
        public RepositoryEmpresa(MyDB myDB) : base(myDB) { }
    }
}

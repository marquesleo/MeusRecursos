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

//INSERT INTO consultapsi.empresa
//(id, cnpj, nome, email, celular, telefone, endereco, cep, cidade, bairro)
//VALUES(uuid_generate_v1(), '63.901.314/0001-85', 'SIMONE PISCOLOGIA', 'simone@gmail.com', '', '', '', '', 'NOVA FRIBURGO', 'CENTRO');

    public class RepositoryEmpresa : RepositoryGeneric<Empresa>, IEmpresa
    {
        public RepositoryEmpresa(MyDB myDB) : base(myDB) { }
    }
}

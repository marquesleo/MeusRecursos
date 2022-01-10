using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.Interfaces
{
   public interface InterfaceEmpresaApp : Generics.InterfaceGenericsApp<Empresa>
    {
        Task Incluir(EmpresaViewModel empresaViewModel);

        Task Alterar(Empresa empresa);

        bool IsEmpresaExiste(string cnpj);

      
    }
}

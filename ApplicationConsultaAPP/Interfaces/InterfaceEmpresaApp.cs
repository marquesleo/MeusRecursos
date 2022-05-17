using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;

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

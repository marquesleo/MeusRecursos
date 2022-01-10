using Domain.Consulta.Entities;
using Domain.Consulta.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsultaAPP.Interfaces
{
    public interface InterfacePsicologaApp : Generics.InterfaceGenericsApp<Psicologa>
    {
        Task Incluir(PsicologaViewModel psicologaViewModel);
        Task Alterar(Psicologa psicologa);
        bool IsPacienteExiste(string nome,string email,string telefone);
        Task<List<Psicologa>> ObterPorNome(string nome);
        Task<List<Psicologa>> ObterPsicologos(Guid Empresa_Id);
    }
}

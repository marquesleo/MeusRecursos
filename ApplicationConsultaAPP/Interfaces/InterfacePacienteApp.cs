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
    public interface InterfacePacienteApp : Generics.InterfaceGenericsApp<Paciente>
    {
       
        Task Incluir(PacienteViewModel pacienteViewModel);
        Task Alterar(Paciente paciente);
        bool IsPacienteExiste(string nome,string email,string telefone);
    }
}

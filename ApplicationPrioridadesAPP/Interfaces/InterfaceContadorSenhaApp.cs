using Domain.Prioridades.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.Interfaces.Generics
{
    public interface InterfaceContadorSenhaApp : InterfaceGenericsApp<ContadorDeSenha>
    {
        Task AddContador(ContadorDeSenha senha);
        Task UpdateSenha(ContadorDeSenha senha);
        Task<ContadorDeSenha> GetContadorSenhaById(Guid id, DateTime dtAcesso);
        Task<List<ContadorDeSenha>> GetContadorSenhaById(Guid idSenha);
    }
}

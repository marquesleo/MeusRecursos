using ApplicationPrioridadesAPP.Interfaces.Generics;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.Interfaces;
using Domain.Prioridades.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppContadorSenha : InterfaceContadorSenhaApp
    {

        private readonly IContadorSenha _IContadorSenha;
        private readonly IUsuario _IUsuario;
  


        public AppContadorSenha(IContadorSenha IContadorSenha,
                           IUsuario IUsuario)
        {
            this._IContadorSenha = IContadorSenha;
            this._IUsuario = IUsuario;
        }


        public async Task AddContador(ContadorDeSenha contadorSenha)
        {
            await _IContadorSenha.Add(contadorSenha);
        }

        public async Task Delete(ContadorDeSenha contadorSenha)
        {
            await _IContadorSenha.Delete(contadorSenha);
        }

        public async Task<List<ContadorDeSenha>> FindByCondition(Expression<Func<ContadorDeSenha, bool>> expression)
        {
            return await _IContadorSenha.FindByCondition(expression);
        }

        public async Task<ContadorDeSenha> GetContadorSenhaById(Guid id)
        {
            return await _IContadorSenha.GetEntityById(id);
        }

        public async Task<ContadorDeSenha> GetEntityById(Guid id)
        {
            return await _IContadorSenha.GetEntityById(id);
        }

        public async Task<List<ContadorDeSenha>> List()
        {
            return await _IContadorSenha.List();
        }

        public async Task UpdateSenha(ContadorDeSenha contadorSenha)
        {
            await _IContadorSenha.Update(contadorSenha);
        }
    }
}

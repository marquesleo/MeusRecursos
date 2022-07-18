using ApplicationPrioridadesAPP.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Prioridades.Interface;
using Domain.Prioridades.ViewModels;
using Domain.Prioridades.Entities;

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppSenha : ApplicationPrioridadesAPP.Interfaces.InterfaceSenhaApp
    {
        private readonly Domain.Prioridades.Interface.ISenha _IMinhaSenha;

        private readonly IServiceSenha _IServiceSenha;
        private readonly IServiceUsuario _IServiceUsuario;


        public AppSenha(ISenha IMinhaSenha,
                        IServiceSenha IServiceSenha)
        {
            this._IMinhaSenha = IMinhaSenha;
            this._IServiceSenha =IServiceSenha;
        }
        public async Task Add(Domain.Prioridades.Entities objeto)
        {
            await _IMinhaSenha.Add(objeto);
        }

        public async Task Delete(Senha objeto)
        {
            await _IMinhaSenha.Delete(objeto);
        }

        public async Task<List<MinhaSenha>> FindByCondition(Expression<Func<MinhaSenha, bool>> expression)
        {
          return  await _IMinhaSenha.FindByCondition(expression);
        }

        public async Task<MinhaSenha> GetEntityById(Guid id)
        {
           return await _IMinhaSenha.GetEntityById(id);
        }

        public async Task<List<MinhaSenha>> List()
        {
            return await _IMinhaSenha.List();
        }

        public async Task Update(MinhaSenha objeto)
        {
            await _IMinhaSenha.Update(objeto);
        }
    }
}

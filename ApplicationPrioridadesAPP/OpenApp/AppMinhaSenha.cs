using Domain.Interface;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppMinhaSenha : InterfaceMinhaSenhaApp
    {
        private readonly IMinhaSenha _IMinhaSenha;
        public AppMinhaSenha(IMinhaSenha IMinhaSenha)
        {
            this._IMinhaSenha = IMinhaSenha;
        }
        public async Task Add(MinhaSenha objeto)
        {
            await _IMinhaSenha.Add(objeto);
        }

        public async Task Delete(MinhaSenha objeto)
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

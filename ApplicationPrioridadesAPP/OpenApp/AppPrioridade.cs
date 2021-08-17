using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.OpenApp
{
    public class AppPrioridade : Interfaces.InterfacePrioridadeApp
    {
        private readonly IPrioridade _IPrioridade;
        private readonly IServicePrioridade _IServicePrioridade;

        public AppPrioridade(IPrioridade IPrioridade , IServicePrioridade IServicePrioridade)
        {
            this._IPrioridade = IPrioridade;
            this._IServicePrioridade = IServicePrioridade;
        }
        
        public async Task AddPrioridade(Prioridade prioridade)
        {
            await _IServicePrioridade.AddPrioridade(prioridade);
        }

        public async Task Delete(Prioridade objeto)
        {
            await _IPrioridade.Delete(objeto);
        }

        public async Task<List<Prioridade>> FindByCondition(Expression<Func<Prioridade, bool>> expression)
        {
            return await _IPrioridade.FindByCondition(expression);
        }

        public async Task<Prioridade> GetEntityById(Guid id)
        {
            return await _IPrioridade.GetEntityById(id);
        }

        public async Task<List<Prioridade>> List()
        {
            return await _IPrioridade.List();
        }

        public async Task UpdatePrioridade(Prioridade prioridade)
        {
            await _IServicePrioridade.UpdatePrioridade(prioridade);
        }
    }
}

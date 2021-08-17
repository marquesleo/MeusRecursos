using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using Domain.Services;
using Domain.Prioridades.Validations;
using Notification;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Prioridades.Services
{
    public class ServicesPrioridade : BaseService, IServicePrioridade
    {
        private readonly IPrioridade _IPrioridade;
      
        public ServicesPrioridade(IPrioridade IPrioridade,
                                  INotificador notificador) :base(notificador)
        {
            this._IPrioridade = IPrioridade;
        }
        public async Task AddPrioridade(Prioridade prioridade)
        {
            if (!ExecutarValidacao(new PrioridadeValidation(), prioridade)) return;
               
             await _IPrioridade.Add(prioridade);
            
        }
        public async Task UpdatePrioridade(Prioridade prioridade)
        {
            if (!ExecutarValidacao(new PrioridadeValidation(), prioridade)) return;

            if (_IPrioridade.FindByCondition(  p => p.Descricao == prioridade.Descricao && p.Id != prioridade.Id).Result.Any())
            {
                Notificar("Já existe uma prioridade cadastrada!");
                return;
            }
            await _IPrioridade.Update(prioridade);
        }
    }
}

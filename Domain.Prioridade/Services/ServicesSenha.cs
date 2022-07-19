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
    public class ServicesSenha : BaseService, IServiceSenha
    {
        private readonly ISenha _ISenha;
      
        public ServicesSenha(ISenha ISenha,
                             INotificador notificador) :base(notificador)
        {
            this._ISenha = ISenha;
        }

        public async Task UpdateSenha(Senha senha)
        {
            if (!ExecutarValidacao(new ServiceValidation(), senha)) return;
            if (_ISenha.FindByCondition(  p => p.Descricao == senha.Descricao && p.Id != senha.Id).Result.Any())
            {
                Notificar("Já existe uma prioridade cadastrada!");
                return;
            }
             await _ISenha.Update(senha);
        }

        public async Task AddSenha(Senha Senha)
        {
              if (!ExecutarValidacao(new ServiceValidation(), Senha)) return;
           await _ISenha.Add(Senha);
        }
                  
    }
}

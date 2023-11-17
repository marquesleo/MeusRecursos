using System;
using System.Threading.Tasks;
using Domain.Prioridades.Entities;
using Domain.Prioridades.Interfaces;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.InterfaceServices;
using Domain.Services;
using Notification;

namespace Domain.Prioridades.Services
{
	public class ServiceContadorSenha : BaseService, IServiceContadorSenha
    {
        private readonly IContadorSenha _IContadorSenha;

        public ServiceContadorSenha(IContadorSenha IContadorSenha,
                                  INotificador notificador) : base(notificador)
        {
            this._IContadorSenha = IContadorSenha;
        }

        public async Task AddContador(ContadorDeSenha contador)
        {
            await _IContadorSenha.Add(contador);
        }

        public async Task UpdateContador(ContadorDeSenha contador)
        {
            await _IContadorSenha.Update(contador);
        }
    }
}


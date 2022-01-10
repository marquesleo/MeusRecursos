using Domain.Consulta.Entities;
using Domain.Consulta.Interfaces;
using Domain.Consulta.InterfaceServices;
using Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Consulta.Services
{
    public class ServiceAcesso : BaseService, IServiceAcesso
    {
        private readonly IAcesso _IAcesso;

        public ServiceAcesso(IAcesso IAcesso,
                                  INotificador notificador) : base(notificador)
        {
            this._IAcesso = IAcesso;
        }

        public async Task AlterarAcesso(Acesso acesso)
        {
            await _IAcesso.Update(acesso);
        }

        public async Task IncluirAcesso(Acesso acesso)
        {
            await _IAcesso.Add(acesso);
        }

        public async Task<Acesso> ObterAcessoPorId(Guid id)
        {
            return await _IAcesso.ObterAcessoPorId(id);
        }

        public async Task<Acesso> ObterAcessoPorUsuarioEEmpresa(Guid idUsuario, Guid idEmpresa)
        {
            return await _IAcesso.ObterAcessoPorUsuarioEEmpresa(idUsuario, idEmpresa);
        }
    }
}

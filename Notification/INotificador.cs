using System.Collections.Generic;
using Entities.Notifications;

namespace Notification
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
      
    }
}

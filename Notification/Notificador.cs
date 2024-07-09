using System.Collections.Generic;
using System.Linq;

namespace Notification
{
    public class Notificacador : INotificador
    {
        public Notificacador()
        {
            _notificacoes = new List<Notificacao>();
        }
        private List<Notificacao> _notificacoes;
        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }
        public List<Notificacao> ObterNotificacoes()
        {
            return _notificacoes;
        }
        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }


    }
}

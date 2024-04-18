using Microsoft.AspNetCore.Mvc;
using Notification;
using System.Collections.Generic;
namespace MinhasPrioridades.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;
        public BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }


        protected bool OperacacaoValida()
        {
            return !_notificador.TemNotificacao();
        }

        protected List<Notificacao> Notificar()
        {
          return _notificador.ObterNotificacoes();
         
        }
    }
}

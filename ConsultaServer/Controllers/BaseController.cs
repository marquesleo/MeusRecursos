using Microsoft.AspNetCore.Mvc;
using Notification;

namespace ConsultaServer.Controllers
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
    }
}

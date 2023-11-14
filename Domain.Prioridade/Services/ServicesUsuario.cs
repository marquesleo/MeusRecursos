using Domain.Prioridades.Entities;
using Domain.Prioridades.Interface;
using Domain.Prioridades.InterfaceService;
using Domain.Prioridades.Validations;
using Domain.Services;
using Notification;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Prioridades.Services
{
   public class ServicesUsuario: BaseService, IServiceUsuario
    {
        private readonly IUsuario _IUsuario;

        public ServicesUsuario(IUsuario IUsuario,
                               INotificador notificador) : base(notificador)
        {
            this._IUsuario = IUsuario;
        }

        public async Task AddUsuario(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return;

            await _IUsuario.Add(usuario);
        }

        public async Task UsuarioPrioridade(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return;

            if (_IUsuario.FindByCondition(p => p.Username == usuario.Username && p.Id != usuario.Id).Result.Any())
            {
                Notificar("Já existe um usuário cadastrado com esse login!");
                return;
            }
            await _IUsuario.Update(usuario);
        }
    }
}

using Domain.Consulta.Entities;
using Domain.Consulta.Interface;
using Domain.Consulta.InterfaceService;
using Domain.Consulta.Validations;
using Notification;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Consulta.Services
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

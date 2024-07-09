

using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Exceptions;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using MediatR;
using Notification;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Command
{
    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, UsuarioResponse>
    {


        private readonly IMapper _mapper;
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        private readonly NotificationContext _notificationContext;



        public CreateUsuarioCommandHandler(IMapper mapper,
                                           InterfaceUsuarioApp  interfaceUsuarioApp,
                                           NotificationContext notificationContext)
        {
            this._InterfaceUsuarioApp = interfaceUsuarioApp;
            this._mapper = mapper;
            this._notificationContext = notificationContext;
        }
        public async Task<UsuarioResponse> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var usuario = _mapper.Map<Domain.Prioridades.Entities.Usuario>(request.LoginViewModel);
                if (usuario != null && usuario.Invalid)

                {
                    _notificationContext.AddNotifications(usuario.ValidationResult);
                    return new UsuarioResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                        Message = _notificationContext.GetErros()
                    };
                }
                else
                {
                    await _InterfaceUsuarioApp.AddUsuario(usuario);
                    return new UsuarioResponse
                    {
                        Success = true,
                        Data = _mapper.Map<LoginViewModel>(usuario),

                    };
                }

            }
            catch(UsuarioDuplicadoException ex)
            {
                return new UsuarioResponse
                {
                    ErrorCode = ErrorCodes.USUARIO_DUPLICATE,
                    Message = ErrorCodes.USUARIO_DUPLICATE.ToString(),
                    Success  = false
                };
            }
            catch(UsuarioComEmailExistenteException ex)
            {
                return new UsuarioResponse
                {
                    ErrorCode = ErrorCodes.USUARIO_COM_EMAIL_EXISTENTE,
                    Message = ErrorCodes.USUARIO_COM_EMAIL_EXISTENTE.ToString(),
                    Success = false
                };
            }
            catch (System.Exception ex)
            {

                return new UsuarioResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message,
                    Success = false
                };
            }
           
        }
    }
}

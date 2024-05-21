
using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using Notification;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Queries
{
    public class GetAutenticacaoQueryHandler : IRequestHandler<GetAutenticacaoQuery, LoginResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        private readonly NotificationContext _notificationContext;

        public GetAutenticacaoQueryHandler(InterfaceUsuarioApp interfaceUsuarioApp,
                                           IMapper mapper,
                                           NotificationContext notificationContext)
        {
            this._InterfaceUsuarioApp = interfaceUsuarioApp;
            this._mapper = mapper;
            this._notificationContext = notificationContext;
        }
        public async Task<LoginResponse> Handle(GetAutenticacaoQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var usuario = await _InterfaceUsuarioApp.ObterUsuario(request.Login.Username,
                                                                 request.Login.Password);
                if (usuario != null || usuario.Invalid)

                {
                    _notificationContext.AddNotifications(usuario.ValidationResult);
                    return new LoginResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.MISSING_REQUIRED_INFORMATION,
                        Message = _notificationContext.GetErros()
                    };
                }
                else if (usuario == null || !usuario.IdValido())
                {
                    return new LoginResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.USUARIO_NOT_FOUND,
                        Message = "Usuário ou Senha Inválidos ou Inexistente!"
                    };
                }

                var response = _InterfaceUsuarioApp.Authenticate(usuario,"");
                return new LoginResponse
                {
                    Data = response,
                    Success = true,
                };
            }
            catch (System.Exception ex)
            {

                return new LoginResponse
                {
                    ErrorCode =  ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message,
                    Success = false
                };
            }
        }
    }
}

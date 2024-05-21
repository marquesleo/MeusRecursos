

using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Queries
{
    public class GetUsuarioQueryHandler : IRequestHandler<GetUsuarioQuery, UsuarioResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        public GetUsuarioQueryHandler(InterfaceUsuarioApp InterfaceUsuarioApp,
                                     IMapper mapper)
        {
            this._InterfaceUsuarioApp = InterfaceUsuarioApp;
            this._mapper = mapper;
        }
        public async Task<UsuarioResponse> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var usuario = await _InterfaceUsuarioApp.GetEntityById(request.Id);

                if (usuario == null || usuario.Username == string.Empty)
                {
                    return new UsuarioResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.USUARIO_NOT_FOUND,
                        Message = ErrorCodes.USUARIO_NOT_FOUND.ToString()
                    };

                }
                return new UsuarioResponse
                {
                    Data = _mapper.Map<Domain.Prioridades.ViewModels.LoginViewModel>(usuario),
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new UsuarioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message
                };
            }
           
        }
    }
}

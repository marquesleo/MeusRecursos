using ApplicationPrioridadesAPP.Interfaces;
using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Command
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, UsuarioResponse>
    {

        private readonly InterfaceUsuarioApp _interfaceUsuarioApp;
       
        public DeleteUsuarioCommandHandler(InterfaceUsuarioApp interfaceUsuarioApp)
        {
            _interfaceUsuarioApp = interfaceUsuarioApp;
           
        }
        public async Task<UsuarioResponse> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var usuario = await _interfaceUsuarioApp.ObterUsuario(request.Id);

                if (usuario == null || !usuario.IdValido())
                {
                    return new UsuarioResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.USUARIO_NOT_FOUND
                    };
                }

                await _interfaceUsuarioApp.Delete(usuario);
                return new UsuarioResponse
                {
                    Success = true,
                   
                };
            }
            catch (Exception ex)
            {

                return new UsuarioResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message
                }; ;
            }
        }
    }
}

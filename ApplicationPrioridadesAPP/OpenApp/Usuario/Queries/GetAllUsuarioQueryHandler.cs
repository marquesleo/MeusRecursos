

using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using Domain.Prioridades.ViewModels;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Queries
{
    public class GetAllUsuarioQueryHandler : IRequestHandler<GetAllUsuarioQuery, UsuarioResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceUsuarioApp _InterfaceUsuarioApp;
        public GetAllUsuarioQueryHandler(IMapper mapper,
                                         InterfaceUsuarioApp interfaceUsuarioApp)
        {
            this._mapper = mapper;
            this._InterfaceUsuarioApp = interfaceUsuarioApp;
        }


        public async Task<UsuarioResponse> Handle(GetAllUsuarioQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var usuarios =  await _InterfaceUsuarioApp.List();
                if (usuarios != null && usuarios.Any())
                {

                    return new UsuarioResponse
                    {
                        Lista = _mapper.Map<List<LoginViewModel>>(usuarios),
                         Success = true
                    };
                }else
                {
                    return new UsuarioResponse
                    {
                        ErrorCode = ErrorCodes.USUARIO_NOT_FOUND,
                        Success = false
                    };
                }
            }
            catch (System.Exception ex)
            {

                return new UsuarioResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}

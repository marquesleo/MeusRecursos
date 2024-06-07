using ApplicationPrioridadesAPP.Interfaces.Generics;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Queries
{
    public class GetContadorDeSenhaQueryHandler : IRequestHandler<GetContadorDeSenhaQuery, ContadorSenhaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceContadorSenhaApp _InterfaceContadorSenhaApp;
        public GetContadorDeSenhaQueryHandler(IMapper mapper,
                                              InterfaceContadorSenhaApp InterfaceContadorSenhaApp)
        {
            _mapper = mapper;
            _InterfaceContadorSenhaApp = InterfaceContadorSenhaApp;
        }
        public async Task<ContadorSenhaResponse> Handle(GetContadorDeSenhaQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var contadorDeSenha = await _InterfaceContadorSenhaApp.GetContadorSenhaById(request.IdSenha);

                if (contadorDeSenha == null)
                {
                    return new ContadorSenhaResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.CONTADOR_SENHA_NOT_FOUND,
                        Message = ErrorCodes.CONTADOR_SENHA_NOT_FOUND.ToString()
                    };

                }

                return new ContadorSenhaResponse
                {
                    List = _mapper.Map<List<Domain.Prioridades.ViewModels.ContadorSenhaViewModel>>(contadorDeSenha),
                    Success = true
                };
            }
            catch (System.Exception ex)
            {

                return new ContadorSenhaResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Message = ex.Message
                };
            }
          
        }
    }
}

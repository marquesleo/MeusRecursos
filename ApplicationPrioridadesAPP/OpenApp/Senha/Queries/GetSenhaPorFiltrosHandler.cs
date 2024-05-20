using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Senha.Queries
{
    public class GetSenhaPorFiltrosHandler : IRequestHandler<GetSenhaPorFiltros, SenhaResponse>
    {

        private readonly IMapper _mapper;
        private readonly InterfaceSenhaApp _interfaceSenhaApp;

        public GetSenhaPorFiltrosHandler(IMapper mapper,
                                         InterfaceSenhaApp _interfaceSenhaApp
                                         )
        {
            this._interfaceSenhaApp = _interfaceSenhaApp;
            this._mapper = mapper;

        }
        public async Task<SenhaResponse> Handle(GetSenhaPorFiltros request, CancellationToken cancellationToken)
        {
            var senhas = await _interfaceSenhaApp.ObterRegistros(request.UsuarioId.ToString(),request.Descricao);

            if (senhas == null || !senhas.Any())
            {
                return new SenhaResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.SENHA_NOT_FOUND,
                    Message = ErrorCodes.SENHA_NOT_FOUND.ToString()
                };

            }
            return new SenhaResponse
            {
                Lista = _mapper.Map<List<Domain.Prioridades.ViewModels.SenhaViewModel>>(senhas),
                Success = true
            };
        }
    }
}

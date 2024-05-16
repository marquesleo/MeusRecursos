using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeFiltrosQueryHandler : IRequestHandler<GetPrioridadeFiltrosQuery, SenhaResponse>
    {
        private readonly IMapper _mapper;
        private readonly InterfacePrioridadeApp _interfacePrioridadeApp;

        public GetPrioridadeFiltrosQueryHandler(IMapper mapper,
                                             InterfacePrioridadeApp InterfacePrioridadeApp
                                           )
        {
            this._interfacePrioridadeApp = InterfacePrioridadeApp;
            this._mapper = mapper;

        }
        public async Task<SenhaResponse> Handle(GetPrioridadeFiltrosQuery request, CancellationToken cancellationToken)
        {
            var prioridades = await _interfacePrioridadeApp.ObterPrioridade(request.Usuario_Id, request.Feito);

            if (prioridades == null || !prioridades.Any())
            {
                return new SenhaResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.PRIORIDADE_NOT_FOUND,
                    Message = ErrorCodes.PRIORIDADE_NOT_FOUND.ToString()
                };

            }

            return new SenhaResponse
            {
                Lista = _mapper.Map<List<Domain.Prioridades.ViewModels.PrioridadeViewModel>>(prioridades),
                Success = true
            };
        }
    }
}

using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeFiltrosQueryHandler : IRequestHandler<GetPrioridadeFiltrosQuery, PrioridadeResponse>
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
        public async Task<PrioridadeResponse> Handle(GetPrioridadeFiltrosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var prioridades = await _interfacePrioridadeApp.ObterPrioridades(request.Usuario_Id, request.Feito);

                if (prioridades == null || !prioridades.Any())
                {
                    return new PrioridadeResponse
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.PRIORIDADE_NOT_FOUND,
                        Message = ErrorCodes.PRIORIDADE_NOT_FOUND.ToString()
                    };

                }

                return new PrioridadeResponse
                {
                    Lista = _mapper.Map<List<Domain.Prioridades.ViewModels.PrioridadeViewModel>>(prioridades),
                    Success = true
                };
            }
            catch (System.Exception ex)
            {

                return new PrioridadeResponse
                {
                    ErrorCode = ErrorCodes.COULDNOT_STORE_DATA,
                    Success = false,
                    Message = ex.Message
                    

                };
            }
           
        }
    }
}

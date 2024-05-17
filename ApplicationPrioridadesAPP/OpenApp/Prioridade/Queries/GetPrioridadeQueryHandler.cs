using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ApplicationPrioridadesAPP.Interfaces;
using AutoMapper;
using Notification;
using ApplicationPrioridadesAPP.OpenApp.Categoria;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeQueryHandler : IRequestHandler<GetPrioridadeQuery, PrioridadeResponse>
    {
        private readonly IMapper _mapper;
        private readonly InterfacePrioridadeApp _interfacePrioridadeApp;
       
        public GetPrioridadeQueryHandler(IMapper mapper,
                                             InterfacePrioridadeApp InterfacePrioridadeApp
                                           )
        {
            this._interfacePrioridadeApp = InterfacePrioridadeApp;
            this._mapper = mapper;
          
        }
      
        public async Task<PrioridadeResponse> Handle(GetPrioridadeQuery request, CancellationToken cancellationToken)
        {
            var prioridade = await _interfacePrioridadeApp.GetEntityById(request.Id);

            if (prioridade == null || prioridade.Descricao == string.Empty)
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
                Data = _mapper.Map<Domain.Prioridades.ViewModels.PrioridadeViewModel>(prioridade),
                Success = true
            };
        }
    }
}

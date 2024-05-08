using MediatR;
using System.Threading.Tasks;
using System.Threading;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeQueryHandler : IRequestHandler<GetPrioridadeQuery, PrioridadeResponse>
    {
        public async Task<PrioridadeResponse> Handle(GetPrioridadeQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

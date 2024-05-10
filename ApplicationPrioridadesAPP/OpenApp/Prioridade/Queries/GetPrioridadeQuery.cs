
using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeQuery : IRequest<PrioridadeResponse>
    {
        public Guid Id { get; set; }
    }
}

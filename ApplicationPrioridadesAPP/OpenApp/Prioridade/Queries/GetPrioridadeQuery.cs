
using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Queries
{
    public class GetPrioridadeQuery : IRequest<SenhaResponse>
    {
        public Guid Id { get; set; }
    }
}

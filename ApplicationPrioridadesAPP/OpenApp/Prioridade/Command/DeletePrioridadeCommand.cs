
using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class DeletePrioridadeCommand : IRequest<PrioridadeResponse>
    {
        public Guid Id { get; set; }
    }
}

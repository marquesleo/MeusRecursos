
using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class DeletePrioridadeCommand : IRequest<SenhaResponse>
    {
        public Guid Id { get; set; }
    }
}



using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class UpdatePrioridadeCommand : IRequest<SenhaResponse>
    {
        public PrioridadeViewModel PrioridadeViewModel { get; set; }
    }
}

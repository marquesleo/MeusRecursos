

using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class DownPrioridadeCommand : IRequest<PrioridadeResponse>
    {
        public PrioridadeViewModel PrioridadeViewModel { get; set; }
    }
}

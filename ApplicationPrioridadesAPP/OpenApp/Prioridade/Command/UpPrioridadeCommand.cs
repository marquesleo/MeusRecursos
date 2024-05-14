using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class UpPrioridadeCommand : IRequest<PrioridadeResponse>
    {
        public PrioridadeViewModel PrioridadeViewModel { get; set; }
    }
}



using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class DownPrioridadeCommand : IRequest<SenhaResponse>
    {
        public PrioridadeViewModel PrioridadeViewModel { get; set; }
    }
}

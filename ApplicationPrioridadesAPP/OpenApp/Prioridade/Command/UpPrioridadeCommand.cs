using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
    public class UpPrioridadeCommand : IRequest<SenhaResponse>
    {
        public PrioridadeViewModel PrioridadeViewModel { get; set; }
    }
}

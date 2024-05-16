using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Prioridade.Command
{
	public class CreatePrioridadeCommand : IRequest<SenhaResponse>
	{
		public PrioridadeViewModel PrioridadeViewModel { get; set; }
	}
}


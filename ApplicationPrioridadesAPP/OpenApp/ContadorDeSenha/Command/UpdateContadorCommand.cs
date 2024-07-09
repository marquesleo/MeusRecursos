
using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.ContadorDeSenha.Command
{
    public class UpdateContadorCommand : IRequest<ContadorSenhaResponse>
    {
        public ContadorSenhaViewModel ContadorSenhaViewModel { get; set; }
    }
}

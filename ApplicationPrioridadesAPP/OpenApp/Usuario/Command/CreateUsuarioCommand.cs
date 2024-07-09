

using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using Domain.Prioridades.ViewModels;
using MediatR;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Command
{
    public class CreateUsuarioCommand : IRequest<UsuarioResponse>
    {
        public LoginViewModel LoginViewModel { get; set; }  
    }
}

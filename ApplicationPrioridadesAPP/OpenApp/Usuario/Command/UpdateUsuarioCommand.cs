using ApplicationPrioridadesAPP.OpenApp.Usuario.Queries;
using Domain.Prioridades.ViewModels;
using MediatR;
using System;

namespace ApplicationPrioridadesAPP.OpenApp.Usuario.Command
{
    public class UpdateUsuarioCommand : IRequest<UsuarioResponse>
    {
        public LoginViewModel LoginViewModel { get; set; }
        public Guid Id { get; set; }
    }
}
